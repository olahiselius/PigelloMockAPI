var builder = WebApplication.CreateBuilder(args);

// Add MCP Server with HTTP transport
builder.Services.AddMcpServer()
    .WithHttpTransport() // Använd streamable HTTP för Azure App Service
    .WithToolsFromAssembly(); // Ladda alla klasser markerade med [McpServerToolType]

// Konfigurera HttpClient för Pigello Mock API
builder.Services.AddHttpClient("PigelloMockAPI", client =>
{
    var baseUrl = builder.Configuration["PigelloMockAPI:BaseUrl"] ?? "http://localhost:5059";
    client.BaseAddress = new Uri(baseUrl);
    client.Timeout = TimeSpan.FromSeconds(30);
});

// Lägg till CORS för att tillåta åtkomst från GitHub Copilot och webbläsare
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// Add services to the container.
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// Aktivera CORS middleware
app.UseCors();

// Mappa MCP endpoint
app.MapMcp("/api/mcp");

// Hälsokontroll endpoint för Azure App Service
app.MapGet("/health", () => Results.Ok(new { status = "healthy", timestamp = DateTime.UtcNow }))
   .WithName("HealthCheck");

// Info endpoint för att visa tillgängliga MCP tools
app.MapGet("/", () => Results.Ok(new 
{ 
    service = "Pigello MCP Server",
    version = "1.0.0",
    mcpEndpoint = "/api/mcp",
    description = "MCP server för Pigello fastighetshantering",
    mockApiUrl = builder.Configuration["PigelloMockAPI:BaseUrl"] ?? "http://localhost:5059"
}))
.WithName("ServiceInfo");

app.Run();
