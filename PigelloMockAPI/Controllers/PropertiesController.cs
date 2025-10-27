using Microsoft.AspNetCore.Mvc;
using PigelloMockAPI.Data;
using PigelloMockAPI.Models;

namespace PigelloMockAPI.Controllers;

/// <summary>
/// Endpoints för att hantera fastigheter
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class PropertiesController : ControllerBase
{
    private readonly InMemoryDataStore _dataStore;

    public PropertiesController(InMemoryDataStore dataStore)
    {
        _dataStore = dataStore;
    }

    /// <summary>
    /// Hämta alla fastigheter
    /// </summary>
    /// <returns>Lista med fastigheter</returns>
    [HttpGet]
    public ActionResult<IEnumerable<Property>> GetProperties()
    {
        return Ok(_dataStore.Properties);
    }

    /// <summary>
    /// Hämta en specifik fastighet
    /// </summary>
    /// <param name="id">ID för fastigheten</param>
    /// <returns>Fastighet om den hittas</returns>
    [HttpGet("{id}")]
    public ActionResult<Property> GetProperty(Guid id)
    {
        var property = _dataStore.Properties.FirstOrDefault(p => p.Id == id);
        if (property == null)
            return NotFound();

        return Ok(property);
    }

    /// <summary>
    /// Skapa en ny fastighet
    /// </summary>
    /// <param name="property">Fastighet att skapa</param>
    /// <returns>Den nyskapade fastigheten</returns>
    [HttpPost]
    public ActionResult<Property> CreateProperty(Property property)
    {
        property.Id = Guid.NewGuid();
        _dataStore.Properties.Add(property);
        return CreatedAtAction(nameof(GetProperty), new { id = property.Id }, property);
    }

    /// <summary>
    /// Uppdatera en befintlig fastighet
    /// </summary>
    /// <param name="id">ID för fastigheten</param>
    /// <param name="updatedProperty">Uppdaterad fastighetsinformation</param>
    /// <returns>Den uppdaterade fastigheten</returns>
    [HttpPut("{id}")]
    public ActionResult<Property> UpdateProperty(Guid id, Property updatedProperty)
    {
        var existingProperty = _dataStore.Properties.FirstOrDefault(p => p.Id == id);
        if (existingProperty == null)
            return NotFound();

        existingProperty.Name = updatedProperty.Name;
        existingProperty.Address = updatedProperty.Address;
        existingProperty.City = updatedProperty.City;
        existingProperty.PostalCode = updatedProperty.PostalCode;
        existingProperty.PropertyDesignation = updatedProperty.PropertyDesignation;
        existingProperty.Description = updatedProperty.Description;

        return Ok(existingProperty);
    }

    /// <summary>
    /// Ta bort en fastighet
    /// </summary>
    /// <param name="id">ID för fastigheten</param>
    /// <returns>Inget innehåll</returns>
    [HttpDelete("{id}")]
    public ActionResult DeleteProperty(Guid id)
    {
        var property = _dataStore.Properties.FirstOrDefault(p => p.Id == id);
        if (property == null)
            return NotFound();

        _dataStore.Properties.Remove(property);
        return NoContent();
    }
}
