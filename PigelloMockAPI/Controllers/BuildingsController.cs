using Microsoft.AspNetCore.Mvc;
using PigelloMockAPI.Data;
using PigelloMockAPI.Models;

namespace PigelloMockAPI.Controllers;

/// <summary>
/// Endpoints för att hantera byggnader
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class BuildingsController : ControllerBase
{
    private readonly InMemoryDataStore _dataStore;

    public BuildingsController(InMemoryDataStore dataStore)
    {
        _dataStore = dataStore;
    }

    /// <summary>
    /// Hämta alla byggnader med valfri filtrering
    /// </summary>
    /// <param name="propertyId">Filtrera på fastighet</param>
    /// <returns>Lista med byggnader</returns>
    [HttpGet]
    public ActionResult<IEnumerable<Building>> GetBuildings([FromQuery] Guid? propertyId = null)
    {
        var buildings = _dataStore.Buildings.AsEnumerable();

        if (propertyId.HasValue)
            buildings = buildings.Where(b => b.PropertyId == propertyId.Value);

        return Ok(buildings.ToList());
    }

    /// <summary>
    /// Hämta en specifik byggnad
    /// </summary>
    /// <param name="id">ID för byggnaden</param>
    /// <returns>Byggnad om den hittas</returns>
    [HttpGet("{id}")]
    public ActionResult<Building> GetBuilding(Guid id)
    {
        var building = _dataStore.Buildings.FirstOrDefault(b => b.Id == id);
        if (building == null)
            return NotFound();

        return Ok(building);
    }

    /// <summary>
    /// Hämta alla rum i en byggnad
    /// </summary>
    /// <param name="id">ID för byggnaden</param>
    /// <returns>Lista med rum i byggnaden</returns>
    [HttpGet("{id}/rooms")]
    public ActionResult<IEnumerable<Room>> GetBuildingRooms(Guid id)
    {
        var building = _dataStore.Buildings.FirstOrDefault(b => b.Id == id);
        if (building == null)
            return NotFound();

        var rooms = _dataStore.Rooms.Where(r => r.BuildingId == id);
        return Ok(rooms.ToList());
    }

    /// <summary>
    /// Skapa en ny byggnad
    /// </summary>
    /// <param name="building">Byggnad att skapa</param>
    /// <returns>Den nyskapade byggnaden</returns>
    [HttpPost]
    public ActionResult<Building> CreateBuilding(Building building)
    {
        building.Id = Guid.NewGuid();
        _dataStore.Buildings.Add(building);
        return CreatedAtAction(nameof(GetBuilding), new { id = building.Id }, building);
    }

    /// <summary>
    /// Uppdatera en befintlig byggnad
    /// </summary>
    /// <param name="id">ID för byggnaden</param>
    /// <param name="updatedBuilding">Uppdaterad byggnadsinformation</param>
    /// <returns>Den uppdaterade byggnaden</returns>
    [HttpPut("{id}")]
    public ActionResult<Building> UpdateBuilding(Guid id, Building updatedBuilding)
    {
        var existingBuilding = _dataStore.Buildings.FirstOrDefault(b => b.Id == id);
        if (existingBuilding == null)
            return NotFound();

        existingBuilding.Name = updatedBuilding.Name;
        existingBuilding.Address = updatedBuilding.Address;
        existingBuilding.PropertyId = updatedBuilding.PropertyId;
        existingBuilding.ConstructionYear = updatedBuilding.ConstructionYear;
        existingBuilding.NumberOfFloors = updatedBuilding.NumberOfFloors;
        existingBuilding.BuildingType = updatedBuilding.BuildingType;
        existingBuilding.Description = updatedBuilding.Description;

        return Ok(existingBuilding);
    }

    /// <summary>
    /// Ta bort en byggnad
    /// </summary>
    /// <param name="id">ID för byggnaden</param>
    /// <returns>Inget innehåll</returns>
    [HttpDelete("{id}")]
    public ActionResult DeleteBuilding(Guid id)
    {
        var building = _dataStore.Buildings.FirstOrDefault(b => b.Id == id);
        if (building == null)
            return NotFound();

        _dataStore.Buildings.Remove(building);
        return NoContent();
    }
}
