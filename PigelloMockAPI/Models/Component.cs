namespace PigelloMockAPI.Models;

/// <summary>
/// Komponent installerad i ett rum (kylskåp, spis, tvättmaskin, blandare etc.)
/// </summary>
public class Component
{
    /// <summary>Unikt ID för komponenten</summary>
    public Guid Id { get; set; }
    
    /// <summary>ID för rum där komponenten är installerad</summary>
    public Guid RoomId { get; set; }
    
    /// <summary>ID för komponentmodell (typ av komponent)</summary>
    public Guid ComponentModelId { get; set; }
    
    /// <summary>Datum när komponenten installerades</summary>
    public DateTime InstallationDate { get; set; }
    
    /// <summary>Serienummer för komponenten</summary>
    public string? SerialNumber { get; set; }
    
    /// <summary>Aktuell status (0=Active, 1=Inactive, 2=Maintenance, 3=Defect, 4=Replaced)</summary>
    public ComponentStatus Status { get; set; }
    
    /// <summary>Datum när garantin går ut</summary>
    public DateTime? WarrantyExpirationDate { get; set; }
    
    /// <summary>Anteckningar om komponenten</summary>
    public string? Notes { get; set; }
}

/// <summary>
/// Status för en komponent
/// </summary>
public enum ComponentStatus
{
    /// <summary>Aktiv och fungerar normalt</summary>
    Active,
    /// <summary>Inaktiv/ej i bruk</summary>
    Inactive,
    /// <summary>Under underhåll</summary>
    Maintenance,
    /// <summary>Trasig/defekt</summary>
    Defect,
    /// <summary>Utbytt</summary>
    Replaced
}
