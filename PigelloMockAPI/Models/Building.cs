namespace PigelloMockAPI.Models;

public class Building
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public Guid PropertyId { get; set; }
    public int? ConstructionYear { get; set; }
    public int? NumberOfFloors { get; set; }
    public string BuildingType { get; set; } = string.Empty;
    public string? Description { get; set; }
}
