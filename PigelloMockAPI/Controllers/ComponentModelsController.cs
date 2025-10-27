using Microsoft.AspNetCore.Mvc;
using PigelloMockAPI.Data;
using PigelloMockAPI.Models;

namespace PigelloMockAPI.Controllers;

/// <summary>
/// Endpoints för att hantera komponentmodeller (mallar för komponenter som kylskåp, spisar, tvättmaskiner etc.)
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class ComponentModelsController : ControllerBase
{
    private readonly InMemoryDataStore _dataStore;

    public ComponentModelsController(InMemoryDataStore dataStore)
    {
        _dataStore = dataStore;
    }

    /// <summary>
    /// Hämta alla komponentmodeller med valfri filtrering
    /// </summary>
    /// <param name="category">Filtrera på kategori (t.ex. Vitvaror, VVS, Ventilation)</param>
    /// <returns>Lista med komponentmodeller</returns>
    [HttpGet]
    public ActionResult<IEnumerable<ComponentModel>> GetComponentModels([FromQuery] string? category = null)
    {
        var models = _dataStore.ComponentModels.AsEnumerable();

        if (!string.IsNullOrEmpty(category))
            models = models.Where(cm => cm.Category.Equals(category, StringComparison.OrdinalIgnoreCase));

        return Ok(models.ToList());
    }

    [HttpGet("{id}")]
    public ActionResult<ComponentModel> GetComponentModel(Guid id)
    {
        var model = _dataStore.ComponentModels.FirstOrDefault(cm => cm.Id == id);
        if (model == null)
            return NotFound();

        return Ok(model);
    }

    /// <summary>
    /// Skapa en ny komponentmodell
    /// </summary>
    /// <param name="model">Komponentmodell att skapa</param>
    /// <returns>Den nyskapade komponentmodellen</returns>
    /// <response code="201">Komponentmodellen skapades</response>
    [HttpPost]
    public ActionResult<ComponentModel> CreateComponentModel(ComponentModel model)
    {
        model.Id = Guid.NewGuid();
        _dataStore.ComponentModels.Add(model);
        return CreatedAtAction(nameof(GetComponentModel), new { id = model.Id }, model);
    }

    [HttpPut("{id}")]
    public ActionResult<ComponentModel> UpdateComponentModel(Guid id, ComponentModel updatedModel)
    {
        var existingModel = _dataStore.ComponentModels.FirstOrDefault(cm => cm.Id == id);
        if (existingModel == null)
            return NotFound();

        existingModel.Name = updatedModel.Name;
        existingModel.Category = updatedModel.Category;
        existingModel.Manufacturer = updatedModel.Manufacturer;
        existingModel.ModelNumber = updatedModel.ModelNumber;
        existingModel.ExpectedLifespanMonths = updatedModel.ExpectedLifespanMonths;
        existingModel.Description = updatedModel.Description;
        existingModel.Specifications = updatedModel.Specifications;

        return Ok(existingModel);
    }

    [HttpDelete("{id}")]
    public ActionResult DeleteComponentModel(Guid id)
    {
        var model = _dataStore.ComponentModels.FirstOrDefault(cm => cm.Id == id);
        if (model == null)
            return NotFound();

        _dataStore.ComponentModels.Remove(model);
        return NoContent();
    }
}
