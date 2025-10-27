namespace PigelloMockAPI.Models;

/// <summary>
/// Mall/typ av komponent (t.ex. Electrolux Kylskåp ERF3307AOX)
/// </summary>
public class ComponentModel
{
    /// <summary>Unikt ID för komponentmodellen</summary>
    public Guid Id { get; set; }
    
    /// <summary>Namn på komponentmodellen</summary>
    public string Name { get; set; } = string.Empty;
    
    /// <summary>Kategori (t.ex. Vitvaror, VVS, Ventilation, El)</summary>
    public string Category { get; set; } = string.Empty;
    
    /// <summary>Tillverkare</summary>
    public string? Manufacturer { get; set; }
    
    /// <summary>Modellnummer</summary>
    public string? ModelNumber { get; set; }
    
    /// <summary>Förväntad livslängd i månader</summary>
    public int? ExpectedLifespanMonths { get; set; }
    
    /// <summary>Beskrivning av komponentmodellen</summary>
    public string? Description { get; set; }
    
    /// <summary>Specifikationer (t.ex. energiklass, kapacitet, storlek)</summary>
    public Dictionary<string, string>? Specifications { get; set; }
}
