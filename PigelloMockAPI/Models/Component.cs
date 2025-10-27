namespace PigelloMockAPI.Models;

public class Component
{
    public Guid Id { get; set; }
    public Guid RoomId { get; set; }
    public Guid ComponentModelId { get; set; }
    public DateTime InstallationDate { get; set; }
    public string? SerialNumber { get; set; }
    public ComponentStatus Status { get; set; }
    public DateTime? WarrantyExpirationDate { get; set; }
    public string? Notes { get; set; }
}

public enum ComponentStatus
{
    Active,
    Inactive,
    Maintenance,
    Defect,
    Replaced
}
