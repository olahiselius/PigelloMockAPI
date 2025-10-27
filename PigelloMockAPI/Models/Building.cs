namespace PigelloMockAPI.Models;

/// <summary>
/// Byggnad som tillhör en fastighet
/// </summary>
public class Building
{
    /// <summary>Unikt ID för byggnaden</summary>
    public Guid Id { get; set; }
    
    /// <summary>Namn på byggnaden</summary>
    public string Name { get; set; } = string.Empty;
    
    /// <summary>Adress till byggnaden</summary>
    public string Address { get; set; } = string.Empty;
    
    /// <summary>ID för fastighet som byggnaden tillhör</summary>
    public Guid PropertyId { get; set; }
    
    /// <summary>Årtal då byggnaden uppfördes</summary>
    public int? ConstructionYear { get; set; }
    
    /// <summary>Antal våningar i byggnaden</summary>
    public int? NumberOfFloors { get; set; }
    
    /// <summary>Typ av byggnad (t.ex. Flerbostadshus, Lokal)</summary>
    public string BuildingType { get; set; } = string.Empty;
    
    /// <summary>Beskrivning av byggnaden</summary>
    public string? Description { get; set; }
}
