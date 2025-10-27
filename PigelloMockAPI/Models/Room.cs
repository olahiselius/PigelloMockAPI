namespace PigelloMockAPI.Models;

/// <summary>
/// Rum i en byggnad (lägenhet, lokal, tvättstuga etc.)
/// </summary>
public class Room
{
    /// <summary>Unikt ID för rummet</summary>
    public Guid Id { get; set; }
    
    /// <summary>Namn på rummet (t.ex. Kök, Vardagsrum)</summary>
    public string Name { get; set; } = string.Empty;
    
    /// <summary>ID för byggnad som rummet tillhör</summary>
    public Guid BuildingId { get; set; }
    
    /// <summary>Lägenhetsnummer eller lokalnummer</summary>
    public string RoomNumber { get; set; } = string.Empty;
    
    /// <summary>Våningsplan (0=källare, 1=bottenvåning etc.)</summary>
    public int? Floor { get; set; }
    
    /// <summary>Typ av rum (t.ex. Kök, Badrum, Vardagsrum, Gemensamt utrymme)</summary>
    public string RoomType { get; set; } = string.Empty;
    
    /// <summary>Area i kvadratmeter</summary>
    public double? Area { get; set; }
    
    /// <summary>Beskrivning av rummet</summary>
    public string? Description { get; set; }
}
