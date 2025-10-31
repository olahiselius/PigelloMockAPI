namespace PigelloMockAPI.Models;

/// <summary>
/// Hyresgäst/kund som hyr en eller flera fastigheter/lägenheter
/// </summary>
public class Tenant
{
    /// <summary>Unikt ID för hyresgästen</summary>
    public Guid Id { get; set; }
    
    /// <summary>Förnamn</summary>
    public string FirstName { get; set; } = string.Empty;
    
    /// <summary>Efternamn</summary>
    public string LastName { get; set; } = string.Empty;
    
    /// <summary>Fullständigt namn (läsbart)</summary>
    public string FullName => $"{FirstName} {LastName}".Trim();
    
    /// <summary>Personnummer (SSN)</summary>
    public string Ssn { get; set; } = string.Empty;
    
    /// <summary>E-postadress</summary>
    public string Email { get; set; } = string.Empty;
    
    /// <summary>Telefonnummer</summary>
    public string PhoneNumber { get; set; } = string.Empty;
    
    /// <summary>Födelsedatum</summary>
    public DateTime? BirthDate { get; set; }
    
    /// <summary>Är hyresgästen aktiv</summary>
    public bool IsActive { get; set; }
    
    /// <summary>Datum då hyresgästen blir aktiv (från)</summary>
    public DateTime? ActiveFrom { get; set; }
    
    /// <summary>Datum då hyresgästen slutar vara aktiv (till)</summary>
    public DateTime? ActiveTo { get; set; }
    
    /// <summary>Organisation som hyresgästen tillhör</summary>
    public Guid? OrganizationId { get; set; }
    
    /// <summary>Skapad tidpunkt</summary>
    public DateTime CreatedAt { get; set; }
    
    /// <summary>Senaste inloggning</summary>
    public DateTime? LastLogin { get; set; }
    
    /// <summary>Företagsnamn (om företagskund)</summary>
    public string? CorporateName { get; set; }
    
    /// <summary>Organisationsnummer (om företagskund)</summary>
    public string? OrgNo { get; set; }
    
    /// <summary>Är detta ett företag</summary>
    public bool IsCompany { get; set; }
    
    /// <summary>Faktureringsadress</summary>
    public string? InvoiceAddress { get; set; }
    
    /// <summary>E-post för fakturor</summary>
    public string? InvoiceEmail { get; set; }
    
    /// <summary>Anmärkningar/noteringar</summary>
    public string? Notes { get; set; }
}
