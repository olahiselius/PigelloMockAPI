using Microsoft.AspNetCore.Mvc;
using PigelloMockAPI.Data;
using PigelloMockAPI.Models;

namespace PigelloMockAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PropertiesController : ControllerBase
{
    private readonly InMemoryDataStore _dataStore;

    public PropertiesController(InMemoryDataStore dataStore)
    {
        _dataStore = dataStore;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Property>> GetProperties()
    {
        return Ok(_dataStore.Properties);
    }

    [HttpGet("{id}")]
    public ActionResult<Property> GetProperty(Guid id)
    {
        var property = _dataStore.Properties.FirstOrDefault(p => p.Id == id);
        if (property == null)
            return NotFound();

        return Ok(property);
    }

    [HttpPost]
    public ActionResult<Property> CreateProperty(Property property)
    {
        property.Id = Guid.NewGuid();
        _dataStore.Properties.Add(property);
        return CreatedAtAction(nameof(GetProperty), new { id = property.Id }, property);
    }

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
