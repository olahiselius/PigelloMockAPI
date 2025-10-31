using PigelloMockAPI.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Register InMemoryDataStore as singleton to persist data during application lifetime
builder.Services.AddSingleton<InMemoryDataStore>();

builder.Services.AddControllers();

// Add Swagger/OpenAPI support
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Pigello Mock API",
        Version = "v1",
        Description = "Ett mock API som simulerar Pigellos API för utveckling av klienter och MCP-servrar",
        Contact = new Microsoft.OpenApi.Models.OpenApiContact
        {
            Name = "Pigello Mock API"
        }
    });

    // Include XML comments for better documentation
    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = System.IO.Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (System.IO.File.Exists(xmlPath))
    {
        options.IncludeXmlComments(xmlPath);
    }
});

// Add CORS policy for development
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Pigello Mock API v1");
    // Swagger UI at root in development, at /swagger in production
    options.RoutePrefix = app.Environment.IsDevelopment() ? string.Empty : "swagger";
});

if (app.Environment.IsDevelopment())
{
    app.UseCors("AllowAll");
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
