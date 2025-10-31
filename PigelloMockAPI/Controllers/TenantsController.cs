using Microsoft.AspNetCore.Mvc;
using PigelloMockAPI.Data;
using PigelloMockAPI.Models;

namespace PigelloMockAPI.Controllers;

/// <summary>
/// Endpoints för att hantera hyresgäster (tenants)
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class TenantsController : ControllerBase
{
    private readonly InMemoryDataStore _dataStore;

    public TenantsController(InMemoryDataStore dataStore)
    {
        _dataStore = dataStore;
    }

    /// <summary>
    /// Hämta alla hyresgäster med valfri filtrering
    /// </summary>
    /// <param name="isActive">Filtrera på aktiva/inaktiva hyresgäster</param>
    /// <param name="isCompany">Filtrera på företag/privatpersoner</param>
    /// <param name="organizationId">Filtrera på organisation</param>
    /// <returns>Lista med hyresgäster</returns>
    [HttpGet]
    public ActionResult<IEnumerable<Tenant>> GetTenants(
        [FromQuery] bool? isActive = null,
        [FromQuery] bool? isCompany = null,
        [FromQuery] Guid? organizationId = null)
    {
        var tenants = _dataStore.Tenants.AsEnumerable();

        if (isActive.HasValue)
            tenants = tenants.Where(t => t.IsActive == isActive.Value);

        if (isCompany.HasValue)
            tenants = tenants.Where(t => t.IsCompany == isCompany.Value);

        if (organizationId.HasValue)
            tenants = tenants.Where(t => t.OrganizationId == organizationId.Value);

        return Ok(tenants.ToList());
    }

    /// <summary>
    /// Hämta en specifik hyresgäst baserat på ID
    /// </summary>
    /// <param name="id">Hyresgästens unika ID</param>
    /// <returns>Hyresgästen</returns>
    [HttpGet("{id}")]
    public ActionResult<Tenant> GetTenant(Guid id)
    {
        var tenant = _dataStore.Tenants.FirstOrDefault(t => t.Id == id);
        if (tenant == null)
            return NotFound();

        return Ok(tenant);
    }

    /// <summary>
    /// Skapa en ny hyresgäst
    /// </summary>
    /// <param name="tenant">Hyresgästdata</param>
    /// <returns>Den skapade hyresgästen</returns>
    [HttpPost]
    public ActionResult<Tenant> CreateTenant(Tenant tenant)
    {
        tenant.Id = Guid.NewGuid();
        tenant.CreatedAt = DateTime.UtcNow;
        _dataStore.Tenants.Add(tenant);
        return CreatedAtAction(nameof(GetTenant), new { id = tenant.Id }, tenant);
    }

    /// <summary>
    /// Uppdatera en befintlig hyresgäst
    /// </summary>
    /// <param name="id">Hyresgästens unika ID</param>
    /// <param name="updatedTenant">Uppdaterad hyresgästdata</param>
    /// <returns>Den uppdaterade hyresgästen</returns>
    [HttpPut("{id}")]
    public ActionResult<Tenant> UpdateTenant(Guid id, Tenant updatedTenant)
    {
        var existingTenant = _dataStore.Tenants.FirstOrDefault(t => t.Id == id);
        if (existingTenant == null)
            return NotFound();

        existingTenant.FirstName = updatedTenant.FirstName;
        existingTenant.LastName = updatedTenant.LastName;
        existingTenant.Ssn = updatedTenant.Ssn;
        existingTenant.Email = updatedTenant.Email;
        existingTenant.PhoneNumber = updatedTenant.PhoneNumber;
        existingTenant.BirthDate = updatedTenant.BirthDate;
        existingTenant.IsActive = updatedTenant.IsActive;
        existingTenant.ActiveFrom = updatedTenant.ActiveFrom;
        existingTenant.ActiveTo = updatedTenant.ActiveTo;
        existingTenant.OrganizationId = updatedTenant.OrganizationId;
        existingTenant.LastLogin = updatedTenant.LastLogin;
        existingTenant.CorporateName = updatedTenant.CorporateName;
        existingTenant.OrgNo = updatedTenant.OrgNo;
        existingTenant.IsCompany = updatedTenant.IsCompany;
        existingTenant.InvoiceAddress = updatedTenant.InvoiceAddress;
        existingTenant.InvoiceEmail = updatedTenant.InvoiceEmail;
        existingTenant.Notes = updatedTenant.Notes;

        return Ok(existingTenant);
    }

    /// <summary>
    /// Uppdatera delar av en hyresgäst (partial update)
    /// </summary>
    /// <param name="id">Hyresgästens unika ID</param>
    /// <param name="updatedTenant">Partiell hyresgästdata</param>
    /// <returns>Den uppdaterade hyresgästen</returns>
    [HttpPatch("{id}")]
    public ActionResult<Tenant> PatchTenant(Guid id, Tenant updatedTenant)
    {
        var existingTenant = _dataStore.Tenants.FirstOrDefault(t => t.Id == id);
        if (existingTenant == null)
            return NotFound();

        // Endast uppdatera fält som inte är default-värden
        if (!string.IsNullOrEmpty(updatedTenant.FirstName))
            existingTenant.FirstName = updatedTenant.FirstName;
        if (!string.IsNullOrEmpty(updatedTenant.LastName))
            existingTenant.LastName = updatedTenant.LastName;
        if (!string.IsNullOrEmpty(updatedTenant.Email))
            existingTenant.Email = updatedTenant.Email;
        if (updatedTenant.IsActive != existingTenant.IsActive)
            existingTenant.IsActive = updatedTenant.IsActive;

        return Ok(existingTenant);
    }

    /// <summary>
    /// Ta bort en hyresgäst
    /// </summary>
    /// <param name="id">Hyresgästens unika ID</param>
    /// <returns>204 No Content vid framgång</returns>
    [HttpDelete("{id}")]
    public ActionResult DeleteTenant(Guid id)
    {
        var tenant = _dataStore.Tenants.FirstOrDefault(t => t.Id == id);
        if (tenant == null)
            return NotFound();

        _dataStore.Tenants.Remove(tenant);
        return NoContent();
    }
}
