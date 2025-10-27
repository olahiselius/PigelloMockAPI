namespace PigelloMockAPI.Models;

public class Room
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public Guid BuildingId { get; set; }
    public string RoomNumber { get; set; } = string.Empty;
    public int? Floor { get; set; }
    public string RoomType { get; set; } = string.Empty;
    public double? Area { get; set; }
    public string? Description { get; set; }
}
