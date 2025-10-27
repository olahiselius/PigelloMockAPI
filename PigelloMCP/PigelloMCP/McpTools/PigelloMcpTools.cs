using System.ComponentModel;
using System.Text.Json;
using ModelContextProtocol.Server;

namespace PigelloMCP.McpTools;

/// <summary>
/// MCP verktyg för att interagera med Pigello Mock API
/// </summary>
[McpServerToolType]
public class PigelloMcpTools(IHttpClientFactory httpClientFactory, IConfiguration configuration)
{
    private readonly HttpClient _httpClient = httpClientFactory.CreateClient("PigelloMockAPI");
    private readonly string _mockApiBaseUrl = configuration["PigelloMockAPI:BaseUrl"] ?? "http://localhost:5059";

    [McpServerTool]
    [Description("Hämta alla ärenden (cases) eller filtrera på tilldelad användare, byggnad, fastighet eller status")]
    public async Task<string> GetCases(
        [Description("GUID för användare som ärendet är tilldelat till (optional)")] string? assignedToUserId = null,
        [Description("GUID för byggnad där ärendet finns (optional)")] string? buildingId = null,
        [Description("GUID för fastighet där ärendet finns (optional)")] string? propertyId = null,
        [Description("Status för ärendet: 0=Open, 1=InProgress, 2=Pending, 3=Closed, 4=Cancelled (optional)")] int? status = null)
    {
        var queryParams = new List<string>();
        if (!string.IsNullOrEmpty(assignedToUserId))
            queryParams.Add($"assignedToUserId={assignedToUserId}");
        if (!string.IsNullOrEmpty(buildingId))
            queryParams.Add($"buildingId={buildingId}");
        if (!string.IsNullOrEmpty(propertyId))
            queryParams.Add($"propertyId={propertyId}");
        if (status.HasValue)
            queryParams.Add($"status={status.Value}");

        var url = $"{_mockApiBaseUrl}/api/cases";
        if (queryParams.Any())
            url += "?" + string.Join("&", queryParams);

        var response = await _httpClient.GetAsync(url);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }

    [McpServerTool]
    [Description("Hämta ett specifikt ärende baserat på ID")]
    public async Task<string> GetCase(
        [Description("GUID för ärendet")] string caseId)
    {
        var response = await _httpClient.GetAsync($"{_mockApiBaseUrl}/api/cases/{caseId}");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }

    [McpServerTool]
    [Description("Uppdatera status på ett ärende (t.ex. stänga/avsluta ett ärende)")]
    public async Task<string> UpdateCaseStatus(
        [Description("GUID för ärendet")] string caseId,
        [Description("Ny status: 0=Open, 1=InProgress, 2=Pending, 3=Closed, 4=Cancelled")] int status)
    {
        var payload = JsonSerializer.Serialize(new { status });
        var content = new StringContent(payload, System.Text.Encoding.UTF8, "application/json");
        var response = await _httpClient.PatchAsync($"{_mockApiBaseUrl}/api/cases/{caseId}/status", content);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }

    [McpServerTool]
    [Description("Skapa ett nytt ärende")]
    public async Task<string> CreateCase(
        [Description("Titel på ärendet")] string title,
        [Description("Beskrivning av ärendet")] string description,
        [Description("GUID för användare som ärendet tilldelas")] string assignedToUserId,
        [Description("GUID för byggnad där ärendet finns")] string buildingId,
        [Description("GUID för fastighet där ärendet finns")] string propertyId,
        [Description("Kategori (t.ex. VVS, El, Vitvaror)")] string category,
        [Description("Prioritet: 0=Low, 1=Medium, 2=High, 3=Critical")] int priority = 1,
        [Description("GUID för rum där ärendet finns (optional)")] string? roomId = null)
    {
        var caseData = new
        {
            title,
            description,
            assignedToUserId,
            buildingId,
            propertyId,
            roomId,
            priority,
            category,
            status = 0 // Open
        };

        var payload = JsonSerializer.Serialize(caseData);
        var content = new StringContent(payload, System.Text.Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync($"{_mockApiBaseUrl}/api/cases", content);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }

    [McpServerTool]
    [Description("Hämta alla byggnader eller filtrera på fastighet")]
    public async Task<string> GetBuildings(
        [Description("GUID för fastighet (optional)")] string? propertyId = null)
    {
        var url = $"{_mockApiBaseUrl}/api/buildings";
        if (!string.IsNullOrEmpty(propertyId))
            url += $"?propertyId={propertyId}";

        var response = await _httpClient.GetAsync(url);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }

    [McpServerTool]
    [Description("Hämta alla rum i en byggnad")]
    public async Task<string> GetRooms(
        [Description("GUID för byggnad (optional)")] string? buildingId = null)
    {
        var url = $"{_mockApiBaseUrl}/api/rooms";
        if (!string.IsNullOrEmpty(buildingId))
            url += $"?buildingId={buildingId}";

        var response = await _httpClient.GetAsync(url);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }

    [McpServerTool]
    [Description("Skapa en ny komponentmodell (mall för komponenter)")]
    public async Task<string> CreateComponentModel(
        [Description("Namn på komponentmodellen")] string name,
        [Description("Kategori (t.ex. Vitvaror, VVS, Ventilation)")] string category,
        [Description("Tillverkare")] string manufacturer,
        [Description("Modellnummer")] string modelNumber,
        [Description("Förväntad livslängd i månader")] int expectedLifespanMonths,
        [Description("Beskrivning")] string description)
    {
        var modelData = new
        {
            name,
            category,
            manufacturer,
            modelNumber,
            expectedLifespanMonths,
            description
        };

        var payload = JsonSerializer.Serialize(modelData);
        var content = new StringContent(payload, System.Text.Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync($"{_mockApiBaseUrl}/api/componentmodels", content);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }

    [McpServerTool]
    [Description("Skapa en ny komponent i ett rum")]
    public async Task<string> CreateComponent(
        [Description("GUID för rum där komponenten installeras")] string roomId,
        [Description("GUID för komponentmodell")] string componentModelId,
        [Description("Serienummer")] string serialNumber,
        [Description("Installationsdatum (ISO format: yyyy-MM-dd)")] string installationDate,
        [Description("Status: 0=Active, 1=Inactive, 2=Maintenance, 3=Defect, 4=Replaced")] int status = 0,
        [Description("Anteckningar (optional)")] string? notes = null)
    {
        var componentData = new
        {
            roomId,
            componentModelId,
            serialNumber,
            installationDate,
            status,
            notes
        };

        var payload = JsonSerializer.Serialize(componentData);
        var content = new StringContent(payload, System.Text.Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync($"{_mockApiBaseUrl}/api/components", content);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }

    [McpServerTool]
    [Description("Hämta alla komponenter eller filtrera på rum, byggnad eller status")]
    public async Task<string> GetComponents(
        [Description("GUID för rum (optional)")] string? roomId = null,
        [Description("GUID för byggnad (optional)")] string? buildingId = null,
        [Description("Status: 0=Active, 1=Inactive, 2=Maintenance, 3=Defect, 4=Replaced (optional)")] int? status = null)
    {
        var queryParams = new List<string>();
        if (!string.IsNullOrEmpty(roomId))
            queryParams.Add($"roomId={roomId}");
        if (!string.IsNullOrEmpty(buildingId))
            queryParams.Add($"buildingId={buildingId}");
        if (status.HasValue)
            queryParams.Add($"status={status.Value}");

        var url = $"{_mockApiBaseUrl}/api/components";
        if (queryParams.Any())
            url += "?" + string.Join("&", queryParams);

        var response = await _httpClient.GetAsync(url);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }

    [McpServerTool]
    [Description("Hämta alla användare")]
    public async Task<string> GetUsers()
    {
        var response = await _httpClient.GetAsync($"{_mockApiBaseUrl}/api/users");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }

    [McpServerTool]
    [Description("Hämta alla fastigheter")]
    public async Task<string> GetProperties()
    {
        var response = await _httpClient.GetAsync($"{_mockApiBaseUrl}/api/properties");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }

    [McpServerTool]
    [Description("Hämta alla komponentmodeller eller filtrera på kategori")]
    public async Task<string> GetComponentModels(
        [Description("Kategori att filtrera på (optional)")] string? category = null)
    {
        var url = $"{_mockApiBaseUrl}/api/componentmodels";
        if (!string.IsNullOrEmpty(category))
            url += $"?category={category}";

        var response = await _httpClient.GetAsync(url);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }
}
