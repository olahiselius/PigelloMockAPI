using PigelloMockAPI.Models;
using System.Text.Json;

namespace PigelloMockAPI.Data;

public class InMemoryDataStore
{
    public List<User> Users { get; set; } = new();
    public List<Property> Properties { get; set; } = new();
    public List<Building> Buildings { get; set; } = new();
    public List<Room> Rooms { get; set; } = new();
    public List<Case> Cases { get; set; } = new();
    public List<ComponentModel> ComponentModels { get; set; } = new();
    public List<Component> Components { get; set; } = new();
    public List<Tenant> Tenants { get; set; } = new();

    private readonly string _seedDataPath;

    public InMemoryDataStore(IWebHostEnvironment environment)
    {
        _seedDataPath = Path.Combine(environment.ContentRootPath, "Data", "SeedData");
        LoadSeedData();
    }

    private void LoadSeedData()
    {
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        try
        {
            // Load Users
            try
            {
                var usersJson = File.ReadAllText(Path.Combine(_seedDataPath, "users.json"));
                Users = JsonSerializer.Deserialize<List<User>>(usersJson, options) ?? new();
                Console.WriteLine($"✓ Loaded {Users.Count} users");
            }
            catch (Exception ex) { Console.WriteLine($"✗ Error loading users: {ex.Message}"); }

            // Load Properties
            try
            {
                var propertiesJson = File.ReadAllText(Path.Combine(_seedDataPath, "properties.json"));
                Properties = JsonSerializer.Deserialize<List<Property>>(propertiesJson, options) ?? new();
                Console.WriteLine($"✓ Loaded {Properties.Count} properties");
            }
            catch (Exception ex) { Console.WriteLine($"✗ Error loading properties: {ex.Message}"); }

            // Load Buildings
            try
            {
                var buildingsJson = File.ReadAllText(Path.Combine(_seedDataPath, "buildings.json"));
                Buildings = JsonSerializer.Deserialize<List<Building>>(buildingsJson, options) ?? new();
                Console.WriteLine($"✓ Loaded {Buildings.Count} buildings");
            }
            catch (Exception ex) { Console.WriteLine($"✗ Error loading buildings: {ex.Message}"); }

            // Load Rooms
            try
            {
                var roomsJson = File.ReadAllText(Path.Combine(_seedDataPath, "rooms.json"));
                Rooms = JsonSerializer.Deserialize<List<Room>>(roomsJson, options) ?? new();
                Console.WriteLine($"✓ Loaded {Rooms.Count} rooms");
            }
            catch (Exception ex) { Console.WriteLine($"✗ Error loading rooms: {ex.Message}"); }

            // Load Component Models
            try
            {
                var componentModelsJson = File.ReadAllText(Path.Combine(_seedDataPath, "component-models.json"));
                ComponentModels = JsonSerializer.Deserialize<List<ComponentModel>>(componentModelsJson, options) ?? new();
                Console.WriteLine($"✓ Loaded {ComponentModels.Count} component models");
            }
            catch (Exception ex) { Console.WriteLine($"✗ Error loading component models: {ex.Message}"); }

            // Load Components
            try
            {
                var componentsJson = File.ReadAllText(Path.Combine(_seedDataPath, "components.json"));
                Components = JsonSerializer.Deserialize<List<Component>>(componentsJson, options) ?? new();
                Console.WriteLine($"✓ Loaded {Components.Count} components");
            }
            catch (Exception ex) { Console.WriteLine($"✗ Error loading components: {ex.Message}"); }

            // Load Cases
            try
            {
                var casesJson = File.ReadAllText(Path.Combine(_seedDataPath, "cases.json"));
                Cases = JsonSerializer.Deserialize<List<Case>>(casesJson, options) ?? new();
                Console.WriteLine($"✓ Loaded {Cases.Count} cases");
            }
            catch (Exception ex) { Console.WriteLine($"✗ Error loading cases: {ex.Message}"); }

            // Load Tenants
            try
            {
                var tenantsJson = File.ReadAllText(Path.Combine(_seedDataPath, "tenants.json"));
                Tenants = JsonSerializer.Deserialize<List<Tenant>>(tenantsJson, options) ?? new();
                Console.WriteLine($"✓ Loaded {Tenants.Count} tenants");
            }
            catch (Exception ex) { Console.WriteLine($"✗ Error loading tenants: {ex.Message}"); }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading seed data: {ex.Message}");
        }
    }
}
