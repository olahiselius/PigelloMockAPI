using PigelloMockAPI.Models;

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

    public InMemoryDataStore()
    {
        SeedData();
    }

    private void SeedData()
    {
        // Seed Users
        var user1 = new User
        {
            Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
            FirstName = "Anna",
            LastName = "Andersson",
            Email = "anna.andersson@example.com",
            PhoneNumber = "070-1234567",
            Role = "Fastighetsförvaltare",
            IsActive = true
        };

        var user2 = new User
        {
            Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
            FirstName = "Erik",
            LastName = "Eriksson",
            Email = "erik.eriksson@example.com",
            PhoneNumber = "070-7654321",
            Role = "Tekniker",
            IsActive = true
        };

        Users.AddRange(new[] { user1, user2 });

        // Seed Properties
        var property1 = new Property
        {
            Id = Guid.Parse("33333333-3333-3333-3333-333333333333"),
            Name = "Rosenlund",
            Address = "Rosenlundsvägen 1",
            City = "Stockholm",
            PostalCode = "11820",
            PropertyDesignation = "Rosenlund 1:1",
            Description = "Bostadsfastighet med 3 byggnader"
        };

        var property2 = new Property
        {
            Id = Guid.Parse("44444444-4444-4444-4444-444444444444"),
            Name = "Liljeholm",
            Address = "Liljeholmsvägen 5",
            City = "Stockholm",
            PostalCode = "11761",
            PropertyDesignation = "Liljeholm 2:3",
            Description = "Bostadsfastighet med lokaler"
        };

        Properties.AddRange(new[] { property1, property2 });

        // Seed Buildings
        var building1 = new Building
        {
            Id = Guid.Parse("55555555-5555-5555-5555-555555555555"),
            Name = "Rosenlund Hus A",
            Address = "Rosenlundsvägen 1A",
            PropertyId = property1.Id,
            ConstructionYear = 1985,
            NumberOfFloors = 5,
            BuildingType = "Flerbostadshus",
            Description = "Huvudbyggnad med 20 lägenheter"
        };

        var building2 = new Building
        {
            Id = Guid.Parse("66666666-6666-6666-6666-666666666666"),
            Name = "Rosenlund Hus B",
            Address = "Rosenlundsvägen 1B",
            PropertyId = property1.Id,
            ConstructionYear = 1987,
            NumberOfFloors = 4,
            BuildingType = "Flerbostadshus",
            Description = "Sidobyggnad med 16 lägenheter"
        };

        Buildings.AddRange(new[] { building1, building2 });

        // Seed Rooms
        var room1 = new Room
        {
            Id = Guid.Parse("77777777-7777-7777-7777-777777777777"),
            Name = "Kök",
            BuildingId = building1.Id,
            RoomNumber = "1101",
            Floor = 1,
            RoomType = "Kök",
            Area = 12.5,
            Description = "Standardkök i lägenhet 1101"
        };

        var room2 = new Room
        {
            Id = Guid.Parse("88888888-8888-8888-8888-888888888888"),
            Name = "Badrum",
            BuildingId = building1.Id,
            RoomNumber = "1101",
            Floor = 1,
            RoomType = "Badrum",
            Area = 5.5,
            Description = "Helkaklat badrum"
        };

        var room3 = new Room
        {
            Id = Guid.Parse("99999999-9999-9999-9999-999999999999"),
            Name = "Tvättstuga",
            BuildingId = building1.Id,
            RoomNumber = "Källare",
            Floor = 0,
            RoomType = "Gemensamt utrymme",
            Area = 25.0,
            Description = "Gemensam tvättstuga"
        };

        Rooms.AddRange(new[] { room1, room2, room3 });

        // Seed Component Models
        var componentModel1 = new ComponentModel
        {
            Id = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
            Name = "Electrolux Kylskåp ERF3307AOX",
            Category = "Vitvaror",
            Manufacturer = "Electrolux",
            ModelNumber = "ERF3307AOX",
            ExpectedLifespanMonths = 120,
            Description = "Energieffektivt kylskåp för hushållsbruk",
            Specifications = new Dictionary<string, string>
            {
                { "EnergyClass", "A++" },
                { "Capacity", "318L" },
                { "Width", "595mm" }
            }
        };

        var componentModel2 = new ComponentModel
        {
            Id = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
            Name = "FM Mattsson Nordic+ Blandare",
            Category = "VVS",
            Manufacturer = "FM Mattsson",
            ModelNumber = "Nordic+ 9000",
            ExpectedLifespanMonths = 180,
            Description = "Blandare med temperaturspärr",
            Specifications = new Dictionary<string, string>
            {
                { "Type", "Termostat" },
                { "Finish", "Krom" }
            }
        };

        var componentModel3 = new ComponentModel
        {
            Id = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc"),
            Name = "Miele Professional WTH720WPM",
            Category = "Vitvaror",
            Manufacturer = "Miele",
            ModelNumber = "WTH720WPM",
            ExpectedLifespanMonths = 180,
            Description = "Professionell tvättmaskin för tvättstuga",
            Specifications = new Dictionary<string, string>
            {
                { "EnergyClass", "A+++" },
                { "Capacity", "8kg" },
                { "SpinSpeed", "1600rpm" }
            }
        };

        ComponentModels.AddRange(new[] { componentModel1, componentModel2, componentModel3 });

        // Seed Components
        var component1 = new Component
        {
            Id = Guid.Parse("dddddddd-dddd-dddd-dddd-dddddddddddd"),
            RoomId = room1.Id,
            ComponentModelId = componentModel1.Id,
            InstallationDate = new DateTime(2022, 3, 15),
            SerialNumber = "ELX-2022-001234",
            Status = ComponentStatus.Active,
            WarrantyExpirationDate = new DateTime(2024, 3, 15),
            Notes = "Installerad vid renovering av kök"
        };

        var component2 = new Component
        {
            Id = Guid.Parse("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"),
            RoomId = room2.Id,
            ComponentModelId = componentModel2.Id,
            InstallationDate = new DateTime(2021, 6, 10),
            SerialNumber = "FMM-2021-005678",
            Status = ComponentStatus.Active,
            WarrantyExpirationDate = new DateTime(2026, 6, 10),
            Notes = null
        };

        var component3 = new Component
        {
            Id = Guid.Parse("ffffffff-ffff-ffff-ffff-ffffffffffff"),
            RoomId = room3.Id,
            ComponentModelId = componentModel3.Id,
            InstallationDate = new DateTime(2020, 1, 20),
            SerialNumber = "MIELE-2020-789012",
            Status = ComponentStatus.Active,
            WarrantyExpirationDate = new DateTime(2025, 1, 20),
            Notes = "Del av gemensam tvättstuga"
        };

        Components.AddRange(new[] { component1, component2, component3 });

        // Seed Cases
        var case1 = new Case
        {
            Id = Guid.Parse("c1111111-1111-1111-1111-111111111111"),
            Title = "Vattenläcka i tak",
            Description = "Hyresgäst rapporterar vattenläcka i taket i vardagsrummet",
            Status = CaseStatus.Open,
            Priority = CasePriority.High,
            AssignedToUserId = user1.Id,
            BuildingId = building1.Id,
            PropertyId = property1.Id,
            RoomId = room1.Id,
            CreatedDate = DateTime.Now.AddDays(-2),
            DueDate = DateTime.Now.AddDays(1),
            Category = "VVS",
            ReportedBy = "Hyresgäst Maria Svensson"
        };

        var case2 = new Case
        {
            Id = Guid.Parse("c2222222-2222-2222-2222-222222222222"),
            Title = "Kylskåp fungerar inte",
            Description = "Kylskåpet i lägenhet 1101 kyler inte ordentligt",
            Status = CaseStatus.InProgress,
            Priority = CasePriority.Medium,
            AssignedToUserId = user2.Id,
            BuildingId = building1.Id,
            PropertyId = property1.Id,
            RoomId = room1.Id,
            CreatedDate = DateTime.Now.AddDays(-5),
            DueDate = DateTime.Now.AddDays(2),
            Category = "Vitvaror",
            ReportedBy = "Hyresgäst Lars Pettersson"
        };

        var case3 = new Case
        {
            Id = Guid.Parse("c3333333-3333-3333-3333-333333333333"),
            Title = "Trasig tvättmaskin",
            Description = "Tvättmaskin i tvättstugan startar inte",
            Status = CaseStatus.Open,
            Priority = CasePriority.High,
            AssignedToUserId = user2.Id,
            BuildingId = building1.Id,
            PropertyId = property1.Id,
            RoomId = room3.Id,
            CreatedDate = DateTime.Now.AddDays(-1),
            DueDate = DateTime.Now.AddDays(3),
            Category = "Vitvaror",
            ReportedBy = "Fastighetsförvaltare"
        };

        var case4 = new Case
        {
            Id = Guid.Parse("c4444444-4444-4444-4444-444444444444"),
            Title = "Besiktning av lägenhet",
            Description = "Rutinbesiktning av lägenhet innan inflyttning",
            Status = CaseStatus.Closed,
            Priority = CasePriority.Low,
            AssignedToUserId = user1.Id,
            BuildingId = building2.Id,
            PropertyId = property1.Id,
            CreatedDate = DateTime.Now.AddDays(-10),
            ClosedDate = DateTime.Now.AddDays(-3),
            Category = "Besiktning",
            ReportedBy = "Förvaltning"
        };

        Cases.AddRange(new[] { case1, case2, case3, case4 });
    }
}
