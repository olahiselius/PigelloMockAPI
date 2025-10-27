namespace PigelloMockAPI.Models;

/// <summary>
/// Ärende i fastighetssystemet (felanmälan, besiktning, underhållsarbete etc.)
/// </summary>
public class Case
{
    /// <summary>Unikt ID för ärendet</summary>
    public Guid Id { get; set; }
    
    /// <summary>Titel/rubrik för ärendet</summary>
    public string Title { get; set; } = string.Empty;
    
    /// <summary>Detaljerad beskrivning av ärendet</summary>
    public string Description { get; set; } = string.Empty;
    
    /// <summary>Aktuell status (0=Open, 1=InProgress, 2=Pending, 3=Closed, 4=Cancelled)</summary>
    public CaseStatus Status { get; set; }
    
    /// <summary>Prioritet (0=Low, 1=Medium, 2=High, 3=Critical)</summary>
    public CasePriority Priority { get; set; }
    
    /// <summary>ID för användare som ärendet är tilldelat</summary>
    public Guid? AssignedToUserId { get; set; }
    
    /// <summary>ID för byggnad som ärendet gäller</summary>
    public Guid? BuildingId { get; set; }
    
    /// <summary>ID för fastighet som ärendet gäller</summary>
    public Guid? PropertyId { get; set; }
    
    /// <summary>ID för rum som ärendet gäller</summary>
    public Guid? RoomId { get; set; }
    
    /// <summary>Datum och tid när ärendet skapades</summary>
    public DateTime CreatedDate { get; set; }
    
    /// <summary>Datum och tid när ärendet avslutades</summary>
    public DateTime? ClosedDate { get; set; }
    
    /// <summary>Sista datum för när ärendet ska vara löst</summary>
    public DateTime? DueDate { get; set; }
    
    /// <summary>Kategori för ärendet (t.ex. VVS, El, Vitvaror)</summary>
    public string Category { get; set; } = string.Empty;
    
    /// <summary>Vem som rapporterade ärendet</summary>
    public string ReportedBy { get; set; } = string.Empty;
}

/// <summary>
/// Status för ett ärende
/// </summary>
public enum CaseStatus
{
    /// <summary>Öppet ärende</summary>
    Open,
    /// <summary>Pågående arbete</summary>
    InProgress,
    /// <summary>Väntar på åtgärd</summary>
    Pending,
    /// <summary>Avslutat</summary>
    Closed,
    /// <summary>Avbrutet</summary>
    Cancelled
}

/// <summary>
/// Prioritet för ett ärende
/// </summary>
public enum CasePriority
{
    /// <summary>Låg prioritet</summary>
    Low,
    /// <summary>Normal prioritet</summary>
    Medium,
    /// <summary>Hög prioritet</summary>
    High,
    /// <summary>Kritisk - akut åtgärd krävs</summary>
    Critical
}
