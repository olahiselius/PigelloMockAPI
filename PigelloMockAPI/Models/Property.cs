namespace PigelloMockAPI.Models;

public class Property
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string PostalCode { get; set; } = string.Empty;
    public string PropertyDesignation { get; set; } = string.Empty;
    public string? Description { get; set; }
}
