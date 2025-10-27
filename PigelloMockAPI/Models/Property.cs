namespace PigelloMockAPI.Models;

/// <summary>
/// Fastighet som kan innehålla en eller flera byggnader
/// </summary>
public class Property
{
    /// <summary>Unikt ID för fastigheten</summary>
    public Guid Id { get; set; }
    
    /// <summary>Namn på fastigheten</summary>
    public string Name { get; set; } = string.Empty;
    
    /// <summary>Adress till fastigheten</summary>
    public string Address { get; set; } = string.Empty;
    
    /// <summary>Stad där fastigheten är belägen</summary>
    public string City { get; set; } = string.Empty;
    
    /// <summary>Postnummer</summary>
    public string PostalCode { get; set; } = string.Empty;
    
    /// <summary>Fastighetsbeteckning (t.ex. Stockholm 3:12)</summary>
    public string PropertyDesignation { get; set; } = string.Empty;
    
    /// <summary>Beskrivning av fastigheten</summary>
    public string? Description { get; set; }
}
