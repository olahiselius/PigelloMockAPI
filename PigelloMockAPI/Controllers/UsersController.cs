using Microsoft.AspNetCore.Mvc;
using PigelloMockAPI.Data;
using PigelloMockAPI.Models;

namespace PigelloMockAPI.Controllers;

/// <summary>
/// Endpoints för att hantera användare
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly InMemoryDataStore _dataStore;

    public UsersController(InMemoryDataStore dataStore)
    {
        _dataStore = dataStore;
    }

    /// <summary>
    /// Hämta alla användare med valfri filtrering
    /// </summary>
    /// <param name="isActive">Filtrera på aktiva/inaktiva användare</param>
    /// <returns>Lista med användare</returns>
    [HttpGet]
    public ActionResult<IEnumerable<User>> GetUsers([FromQuery] bool? isActive = null)
    {
        var users = _dataStore.Users.AsEnumerable();

        if (isActive.HasValue)
            users = users.Where(u => u.IsActive == isActive.Value);

        return Ok(users.ToList());
    }

    [HttpGet("{id}")]
    public ActionResult<User> GetUser(Guid id)
    {
        var user = _dataStore.Users.FirstOrDefault(u => u.Id == id);
        if (user == null)
            return NotFound();

        return Ok(user);
    }

    /// <summary>
    /// Hämta inloggad användare (mock - returnerar första aktiva användaren)
    /// </summary>
    /// <returns>Den inloggade användaren</returns>
    [HttpGet("me")]
    public ActionResult<User> GetCurrentUser()
    {
        // Mock: returnera första aktiva användaren
        var user = _dataStore.Users.FirstOrDefault(u => u.IsActive);
        if (user == null)
            return NotFound();

        return Ok(user);
    }

    [HttpPost]
    public ActionResult<User> CreateUser(User user)
    {
        user.Id = Guid.NewGuid();
        _dataStore.Users.Add(user);
        return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
    }

    [HttpPut("{id}")]
    public ActionResult<User> UpdateUser(Guid id, User updatedUser)
    {
        var existingUser = _dataStore.Users.FirstOrDefault(u => u.Id == id);
        if (existingUser == null)
            return NotFound();

        existingUser.FirstName = updatedUser.FirstName;
        existingUser.LastName = updatedUser.LastName;
        existingUser.Email = updatedUser.Email;
        existingUser.PhoneNumber = updatedUser.PhoneNumber;
        existingUser.Role = updatedUser.Role;
        existingUser.IsActive = updatedUser.IsActive;

        return Ok(existingUser);
    }

    [HttpDelete("{id}")]
    public ActionResult DeleteUser(Guid id)
    {
        var user = _dataStore.Users.FirstOrDefault(u => u.Id == id);
        if (user == null)
            return NotFound();

        _dataStore.Users.Remove(user);
        return NoContent();
    }
}
