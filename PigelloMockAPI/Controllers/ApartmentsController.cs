using Microsoft.AspNetCore.Mvc;
using PigelloMockAPI.Data;
using PigelloMockAPI.Models;

namespace PigelloMockAPI.Controllers;

/// <summary>
/// Controller för att hantera apartments/lägenheter
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class ApartmentsController : ControllerBase
{
    private readonly InMemoryDataStore _dataStore;

    public ApartmentsController(InMemoryDataStore dataStore)
    {
        _dataStore = dataStore;
    }

    /// <summary>
    /// Hämtar alla apartments
    /// </summary>
    /// <returns>Lista med alla apartments</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<IEnumerable<Apartment>> GetAll()
    {
        return Ok(_dataStore.Apartments);
    }

    /// <summary>
    /// Hämtar en specifik apartment baserat på ID
    /// </summary>
    /// <param name="id">Apartment ID (GUID)</param>
    /// <returns>Apartment om den hittas</returns>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<Apartment> GetById(Guid id)
    {
        var apartment = _dataStore.Apartments.FirstOrDefault(a => a.Id == id);
        if (apartment == null)
        {
            return NotFound(new { message = $"Apartment med ID {id} hittades inte" });
        }
        return Ok(apartment);
    }

    /// <summary>
    /// Hämtar alla apartments för en specifik byggnad
    /// </summary>
    /// <param name="buildingId">Building ID (GUID)</param>
    /// <returns>Lista med apartments i byggnaden</returns>
    [HttpGet("building/{buildingId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<IEnumerable<Apartment>> GetByBuildingId(Guid buildingId)
    {
        var apartments = _dataStore.Apartments.Where(a => a.BuildingId == buildingId);
        return Ok(apartments);
    }

    /// <summary>
    /// Hämtar apartment för en specifik tenant/hyresgäst
    /// </summary>
    /// <param name="tenantId">Tenant ID (GUID)</param>
    /// <returns>Lista med apartments som hyresgästen hyr</returns>
    [HttpGet("tenant/{tenantId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<IEnumerable<Apartment>> GetByTenantId(Guid tenantId)
    {
        var apartments = _dataStore.Apartments.Where(a => a.TenantId == tenantId);
        return Ok(apartments);
    }

    /// <summary>
    /// Hämtar alla lediga apartments (utan tenant)
    /// </summary>
    /// <returns>Lista med lediga apartments</returns>
    [HttpGet("vacant")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<IEnumerable<Apartment>> GetVacant()
    {
        var vacantApartments = _dataStore.Apartments.Where(a => a.TenantId == null);
        return Ok(vacantApartments);
    }

    /// <summary>
    /// Skapar en ny apartment
    /// </summary>
    /// <param name="apartment">Apartment-objekt att skapa</param>
    /// <returns>Den skapade apartment</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<Apartment> Create([FromBody] Apartment apartment)
    {
        if (apartment.Id == Guid.Empty)
        {
            apartment.Id = Guid.NewGuid();
        }

        apartment.CreatedAt = DateTime.UtcNow;
        _dataStore.Apartments.Add(apartment);
        
        return CreatedAtAction(nameof(GetById), new { id = apartment.Id }, apartment);
    }

    /// <summary>
    /// Uppdaterar en befintlig apartment
    /// </summary>
    /// <param name="id">Apartment ID (GUID)</param>
    /// <param name="updatedApartment">Uppdaterad apartment-data</param>
    /// <returns>Den uppdaterade apartment</returns>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<Apartment> Update(Guid id, [FromBody] Apartment updatedApartment)
    {
        var existingApartment = _dataStore.Apartments.FirstOrDefault(a => a.Id == id);
        if (existingApartment == null)
        {
            return NotFound(new { message = $"Apartment med ID {id} hittades inte" });
        }

        // Uppdatera properties
        existingApartment.ApartmentId = updatedApartment.ApartmentId;
        existingApartment.PopularName = updatedApartment.PopularName;
        existingApartment.BuildingId = updatedApartment.BuildingId;
        existingApartment.TenantId = updatedApartment.TenantId;
        existingApartment.Category = updatedApartment.Category;
        existingApartment.BiArea = updatedApartment.BiArea;
        existingApartment.UsableArea = updatedApartment.UsableArea;
        existingApartment.TemperedArea = updatedApartment.TemperedArea;
        existingApartment.Floor = updatedApartment.Floor;
        existingApartment.Floors = updatedApartment.Floors;
        existingApartment.Status = updatedApartment.Status;
        existingApartment.Archived = updatedApartment.Archived;
        existingApartment.HasBaseRentVat = updatedApartment.HasBaseRentVat;
        existingApartment.LandlordCustomId = updatedApartment.LandlordCustomId;
        existingApartment.CustomDimensionValue = updatedApartment.CustomDimensionValue;
        existingApartment.CustomId = updatedApartment.CustomId;
        existingApartment.Notes = updatedApartment.Notes;

        return Ok(existingApartment);
    }

    /// <summary>
    /// Tar bort en apartment
    /// </summary>
    /// <param name="id">Apartment ID (GUID)</param>
    /// <returns>NoContent om borttagning lyckas</returns>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult Delete(Guid id)
    {
        var apartment = _dataStore.Apartments.FirstOrDefault(a => a.Id == id);
        if (apartment == null)
        {
            return NotFound(new { message = $"Apartment med ID {id} hittades inte" });
        }

        _dataStore.Apartments.Remove(apartment);
        return NoContent();
    }
}
