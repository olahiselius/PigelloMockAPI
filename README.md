# Pigello Mock API & MCP Server

Komplett lösning för Pigello fastighetshantering med Mock API och MCP Server.

## Projektstruktur

```
PigelloMockAPI/
├── PigelloMockAPI/          # Mock API (REST)
│   ├── Controllers/         # 7 API controllers
│   ├── Models/              # 7 datamodeller
│   ├── Data/                # InMemoryDataStore + JSON seed data
│   └── Port: 5059
│
└── PigelloMCP/              # MCP Server (HTTP/SSE)
    ├── McpTools/            # MCP verktyg som anropar Mock API
    ├── Program.cs           # MCP server konfiguration
    └── Port: 5063
```

## Kom igång

### 1. Starta Mock API

```bash
cd PigelloMockAPI/PigelloMockAPI
dotnet run
```

Mock API körs på: `http://localhost:5059`
- Swagger UI: `http://localhost:5059/swagger`
- Test data: 5 users, 4 properties, 6 buildings, 9 rooms, 7 component models, 9 components, 10 cases

### 2. Starta MCP Server

```bash
cd PigelloMCP/PigelloMCP
dotnet run
```

MCP Server körs på: `http://localhost:5063`
- Info: `http://localhost:5063/`
- Health: `http://localhost:5063/health`
- MCP Endpoint: `http://localhost:5063/api/mcp`

## MCP Server Verktyg

MCP servern exponerar 14 verktyg via Model Context Protocol:

### Ärenden (Cases)
- **GetCases** - Hämta ärenden med filtrering (user, building, property, status)
- **GetCase** - Hämta specifikt ärende
- **UpdateCaseStatus** - Uppdatera ärendestatus (stänga/avsluta)
- **CreateCase** - Skapa nytt ärende

### Byggnader & Rum
- **GetBuildings** - Hämta byggnader (optional property filter)
- **GetRooms** - Hämta rum (optional building filter)

### Komponenter
- **GetComponents** - Hämta komponenter (room/building/status filter)
- **CreateComponent** - Skapa komponent i rum
- **GetComponentModels** - Hämta komponentmodeller (category filter)
- **CreateComponentModel** - Skapa komponentmodell

### Övriga
- **GetUsers** - Hämta användare
- **GetProperties** - Hämta fastigheter

## Användningsexempel

### Anslut till MCP Server från GitHub Copilot

Konfigurera i VS Code eller Codespaces:

```json
{
  "mcpServers": {
    "pigello": {
      "type": "sse",
      "url": "http://localhost:5063/api/mcp"
    }
  }
}
```

### Exempel på frågor till Copilot

När MCP servern är konfigurerad:

```
"Visa mina ärenden"
→ Anropar GetCases med din user ID

"Vilka ärenden finns för byggnad Rosenlund?"
→ Filtrerar på buildingId

"Stäng ärende 11111111-1111-1111-1111-111111111111"
→ Anropar UpdateCaseStatus med status=Closed (3)

"Skapa ett ärende om trasig diskmaskin i kök"
→ Anropar CreateCase med lämpliga parametrar

"Vilka komponenter finns i tvättstugan?"
→ Anropar GetComponents filtrerat på roomId
```

## Test Data IDs

För testning, använd dessa IDs:

**Users:**
- Anna Andersson: `11111111-1111-1111-1111-111111111111`
- Erik Eriksson: `22222222-2222-2222-2222-222222222222`
- Maria Svensson (VVS): `33333333-3333-3333-3333-333333333333`

**Properties:**
- Rosenlund: `aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa`
- Liljeholm: `bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb`

**Buildings:**
- Rosenlund Hus A: `11111111-1111-1111-1111-111111111111`
- Rosenlund Hus B: `12222222-2222-2222-2222-222222222222`

**Rooms:**
- Kök (Hus A): `11111111-1111-1111-1111-111111111111`
- Badrum (Hus A): `12222222-2222-2222-2222-222222222222`
- Tvättstuga: `15555555-5555-5555-5555-555555555555`

## Deployment till Azure

### Mock API till Azure App Service

```bash
cd PigelloMockAPI/PigelloMockAPI
az webapp up --name pigello-mock-api --resource-group your-rg --runtime "DOTNET:9.0"
```

### MCP Server till Azure App Service

```bash
cd PigelloMCP/PigelloMCP
az webapp up --name pigello-mcp-server --resource-group your-rg --runtime "DOTNET:9.0"
```

**Konfigurera Application Settings i Azure:**

```bash
az webapp config appsettings set --name pigello-mcp-server --resource-group your-rg \
  --settings PigelloMockAPI__BaseUrl="https://pigello-mock-api.azurewebsites.net"
```

**Uppdatera MCP konfiguration för Azure:**

```json
{
  "mcpServers": {
    "pigello": {
      "type": "sse",
      "url": "https://pigello-mcp-server.azurewebsites.net/api/mcp"
    }
  }
}
```

## Teknisk Stack

- **.NET 9.0**
- **ASP.NET Core Web API**
- **ModelContextProtocol.AspNetCore** (Preview)
- **HTTP/SSE transport** för MCP
- **In-memory database** med JSON seed data
- **CORS enabled** för cross-origin requests
- **Azure App Service ready**

## Endpoints Översikt

### Mock API (Port 5059)
- `GET /api/users` - Lista användare
- `GET /api/properties` - Lista fastigheter
- `GET /api/buildings` - Lista byggnader
- `GET /api/rooms` - Lista rum
- `GET /api/cases` - Lista ärenden
- `GET /api/componentmodels` - Lista komponentmodeller
- `GET /api/components` - Lista komponenter
- ... och alla CRUD operationer

### MCP Server (Port 5063)
- `GET /` - Service information
- `GET /health` - Health check
- `POST /api/mcp` - MCP endpoint (SSE/HTTP)

## Utveckling

```bash
# Restore alla dependencies
dotnet restore

# Bygg båda projekten
dotnet build

# Kör tester
dotnet test

# Publicera för produktion
dotnet publish -c Release
```

## Licens

Intern prototyp för Pigello fastighetshantering.
Mockat API för att simulera delar av Pigellos API. Skapat för att kunna testa att bygga MCP server mot Pigello utan att ha någon Pigello miljö tillgänglig
