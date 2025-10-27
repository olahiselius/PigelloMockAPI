namespace PigelloMockAPI.Models;

public class ComponentModel
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public string? Manufacturer { get; set; }
    public string? ModelNumber { get; set; }
    public int? ExpectedLifespanMonths { get; set; }
    public string? Description { get; set; }
    public Dictionary<string, string>? Specifications { get; set; }
}
