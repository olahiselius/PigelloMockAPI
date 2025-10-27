namespace PigelloMockAPI.Models;

/// <summary>
/// Användare i fastighetssystemet (fastighetsförvaltare, tekniker etc.)
/// </summary>
public class User
{
    /// <summary>Unikt ID för användaren</summary>
    public Guid Id { get; set; }
    
    /// <summary>Förnamn</summary>
    public string FirstName { get; set; } = string.Empty;
    
    /// <summary>Efternamn</summary>
    public string LastName { get; set; } = string.Empty;
    
    /// <summary>E-postadress</summary>
    public string Email { get; set; } = string.Empty;
    
    /// <summary>Telefonnummer</summary>
    public string? PhoneNumber { get; set; }
    
    /// <summary>Roll/titel (t.ex. Fastighetsförvaltare, Tekniker)</summary>
    public string Role { get; set; } = string.Empty;
    
    /// <summary>Om användaren är aktiv i systemet</summary>
    public bool IsActive { get; set; }
}
