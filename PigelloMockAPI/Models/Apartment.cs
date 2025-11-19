namespace PigelloMockAPI.Models;

/// <summary>
/// Apartment (lägenhet eller lokal) - kopplar samman byggnad med hyresgäst
/// </summary>
public class Apartment
{
    /// <summary>Unikt ID för lägenheten (UUID)</summary>
    public Guid Id { get; set; }
    
    /// <summary>Lägenhetsnummer/identifierare (t.ex. 1101, 2A etc.)</summary>
    public string ApartmentId { get; set; } = string.Empty;
    
    /// <summary>Populärt namn eller beskrivande namn</summary>
    public string? PopularName { get; set; }
    
    /// <summary>ID för byggnad som lägenheten tillhör</summary>
    public Guid BuildingId { get; set; }
    
    /// <summary>ID för hyresgäst (tenant) - kan vara null om lägenheten är ledig</summary>
    public Guid? TenantId { get; set; }
    
    /// <summary>Kategori (t.ex. "Lägenhet", "Lokal", "Kontor")</summary>
    public string? Category { get; set; }
    
    /// <summary>BTA (Bruttoarea) i kvadratmeter</summary>
    public double? BiArea { get; set; }
    
    /// <summary>BOA (Boarea/användbar yta) i kvadratmeter</summary>
    public double? UsableArea { get; set; }
    
    /// <summary>Uppvärmd yta i kvadratmeter</summary>
    public double? TemperedArea { get; set; }
    
    /// <summary>Våningsplan</summary>
    public int? Floor { get; set; }
    
    /// <summary>Våningsplan som fritext (t.ex. "3 tr", "BV")</summary>
    public string? Floors { get; set; }
    
    /// <summary>Status (12 = Active enligt Pigello API)</summary>
    public int Status { get; set; } = 12;
    
    /// <summary>Är lägenheten arkiverad</summary>
    public bool Archived { get; set; } = false;
    
    /// <summary>Har lägenheten moms på hyran</summary>
    public bool HasBaseRentVat { get; set; } = false;
    
    /// <summary>Anpassat ID från hyresvärd</summary>
    public string? LandlordCustomId { get; set; }
    
    /// <summary>Anpassat dimensions-värde</summary>
    public string? CustomDimensionValue { get; set; }
    
    /// <summary>Anpassat ID</summary>
    public string? CustomId { get; set; }
    
    /// <summary>Skapad tidpunkt</summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    /// <summary>Anteckningar</summary>
    public string? Notes { get; set; }
}
