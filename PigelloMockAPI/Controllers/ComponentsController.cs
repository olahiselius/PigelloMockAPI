using Microsoft.AspNetCore.Mvc;
using PigelloMockAPI.Data;
using PigelloMockAPI.Models;

namespace PigelloMockAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ComponentsController : ControllerBase
{
    private readonly InMemoryDataStore _dataStore;

    public ComponentsController(InMemoryDataStore dataStore)
    {
        _dataStore = dataStore;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Component>> GetComponents(
        [FromQuery] Guid? roomId = null,
        [FromQuery] Guid? buildingId = null,
        [FromQuery] ComponentStatus? status = null)
    {
        var components = _dataStore.Components.AsEnumerable();

        if (roomId.HasValue)
            components = components.Where(c => c.RoomId == roomId.Value);

        if (buildingId.HasValue)
        {
            var roomsInBuilding = _dataStore.Rooms.Where(r => r.BuildingId == buildingId.Value).Select(r => r.Id);
            components = components.Where(c => roomsInBuilding.Contains(c.RoomId));
        }

        if (status.HasValue)
            components = components.Where(c => c.Status == status.Value);

        return Ok(components.ToList());
    }

    [HttpGet("{id}")]
    public ActionResult<Component> GetComponent(Guid id)
    {
        var component = _dataStore.Components.FirstOrDefault(c => c.Id == id);
        if (component == null)
            return NotFound();

        return Ok(component);
    }

    [HttpPost]
    public ActionResult<Component> CreateComponent(Component component)
    {
        // Validate that room exists
        var room = _dataStore.Rooms.FirstOrDefault(r => r.Id == component.RoomId);
        if (room == null)
            return BadRequest("Room not found");

        // Validate that component model exists
        var componentModel = _dataStore.ComponentModels.FirstOrDefault(cm => cm.Id == component.ComponentModelId);
        if (componentModel == null)
            return BadRequest("Component model not found");

        component.Id = Guid.NewGuid();
        _dataStore.Components.Add(component);
        return CreatedAtAction(nameof(GetComponent), new { id = component.Id }, component);
    }

    [HttpPut("{id}")]
    public ActionResult<Component> UpdateComponent(Guid id, Component updatedComponent)
    {
        var existingComponent = _dataStore.Components.FirstOrDefault(c => c.Id == id);
        if (existingComponent == null)
            return NotFound();

        existingComponent.RoomId = updatedComponent.RoomId;
        existingComponent.ComponentModelId = updatedComponent.ComponentModelId;
        existingComponent.InstallationDate = updatedComponent.InstallationDate;
        existingComponent.SerialNumber = updatedComponent.SerialNumber;
        existingComponent.Status = updatedComponent.Status;
        existingComponent.WarrantyExpirationDate = updatedComponent.WarrantyExpirationDate;
        existingComponent.Notes = updatedComponent.Notes;

        return Ok(existingComponent);
    }

    [HttpDelete("{id}")]
    public ActionResult DeleteComponent(Guid id)
    {
        var component = _dataStore.Components.FirstOrDefault(c => c.Id == id);
        if (component == null)
            return NotFound();

        _dataStore.Components.Remove(component);
        return NoContent();
    }
}
