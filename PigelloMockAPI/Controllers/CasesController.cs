using Microsoft.AspNetCore.Mvc;
using PigelloMockAPI.Data;
using PigelloMockAPI.Models;

namespace PigelloMockAPI.Controllers;

/// <summary>
/// Endpoints för att hantera ärenden i fastighetssystemet
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class CasesController : ControllerBase
{
    private readonly InMemoryDataStore _dataStore;

    public CasesController(InMemoryDataStore dataStore)
    {
        _dataStore = dataStore;
    }

    /// <summary>
    /// Hämta alla ärenden med valfri filtrering
    /// </summary>
    /// <param name="assignedToUserId">Filtrera på användare som ärendet är tilldelat</param>
    /// <param name="buildingId">Filtrera på byggnad</param>
    /// <param name="propertyId">Filtrera på fastighet</param>
    /// <param name="status">Filtrera på status</param>
    /// <returns>Lista med ärenden</returns>
    [HttpGet]
    public ActionResult<IEnumerable<Case>> GetCases(
        [FromQuery] Guid? assignedToUserId = null,
        [FromQuery] Guid? buildingId = null,
        [FromQuery] Guid? propertyId = null,
        [FromQuery] CaseStatus? status = null)
    {
        var cases = _dataStore.Cases.AsEnumerable();

        if (assignedToUserId.HasValue)
            cases = cases.Where(c => c.AssignedToUserId == assignedToUserId.Value);

        if (buildingId.HasValue)
            cases = cases.Where(c => c.BuildingId == buildingId.Value);

        if (propertyId.HasValue)
            cases = cases.Where(c => c.PropertyId == propertyId.Value);

        if (status.HasValue)
            cases = cases.Where(c => c.Status == status.Value);

        return Ok(cases.ToList());
    }

    [HttpGet("{id}")]
    public ActionResult<Case> GetCase(Guid id)
    {
        var caseItem = _dataStore.Cases.FirstOrDefault(c => c.Id == id);
        if (caseItem == null)
            return NotFound();

        return Ok(caseItem);
    }

    [HttpPost]
    public ActionResult<Case> CreateCase(Case newCase)
    {
        newCase.Id = Guid.NewGuid();
        newCase.CreatedDate = DateTime.Now;
        _dataStore.Cases.Add(newCase);
        return CreatedAtAction(nameof(GetCase), new { id = newCase.Id }, newCase);
    }

    [HttpPut("{id}")]
    public ActionResult<Case> UpdateCase(Guid id, Case updatedCase)
    {
        var existingCase = _dataStore.Cases.FirstOrDefault(c => c.Id == id);
        if (existingCase == null)
            return NotFound();

        existingCase.Title = updatedCase.Title;
        existingCase.Description = updatedCase.Description;
        existingCase.Status = updatedCase.Status;
        existingCase.Priority = updatedCase.Priority;
        existingCase.AssignedToUserId = updatedCase.AssignedToUserId;
        existingCase.BuildingId = updatedCase.BuildingId;
        existingCase.PropertyId = updatedCase.PropertyId;
        existingCase.RoomId = updatedCase.RoomId;
        existingCase.DueDate = updatedCase.DueDate;
        existingCase.Category = updatedCase.Category;

        if (updatedCase.Status == CaseStatus.Closed && existingCase.ClosedDate == null)
            existingCase.ClosedDate = DateTime.Now;

        return Ok(existingCase);
    }

    /// <summary>
    /// Uppdatera status på ett ärende
    /// </summary>
    /// <param name="id">ID för ärendet</param>
    /// <param name="status">Ny status (0=Open, 1=InProgress, 2=Pending, 3=Closed, 4=Cancelled)</param>
    /// <returns>Uppdaterat ärende</returns>
    [HttpPatch("{id}/status")]
    public ActionResult<Case> UpdateCaseStatus(Guid id, [FromBody] CaseStatus status)
    {
        var existingCase = _dataStore.Cases.FirstOrDefault(c => c.Id == id);
        if (existingCase == null)
            return NotFound();

        existingCase.Status = status;
        if (status == CaseStatus.Closed && existingCase.ClosedDate == null)
            existingCase.ClosedDate = DateTime.Now;

        return Ok(existingCase);
    }

    [HttpDelete("{id}")]
    public ActionResult DeleteCase(Guid id)
    {
        var caseItem = _dataStore.Cases.FirstOrDefault(c => c.Id == id);
        if (caseItem == null)
            return NotFound();

        _dataStore.Cases.Remove(caseItem);
        return NoContent();
    }
}
