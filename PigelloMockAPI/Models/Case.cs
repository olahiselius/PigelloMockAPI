namespace PigelloMockAPI.Models;

public class Case
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public CaseStatus Status { get; set; }
    public CasePriority Priority { get; set; }
    public Guid? AssignedToUserId { get; set; }
    public Guid? BuildingId { get; set; }
    public Guid? PropertyId { get; set; }
    public Guid? RoomId { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? ClosedDate { get; set; }
    public DateTime? DueDate { get; set; }
    public string Category { get; set; } = string.Empty;
    public string ReportedBy { get; set; } = string.Empty;
}

public enum CaseStatus
{
    Open,
    InProgress,
    Pending,
    Closed,
    Cancelled
}

public enum CasePriority
{
    Low,
    Medium,
    High,
    Critical
}
