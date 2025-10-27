# Pigello MCP Server

En Model Context Protocol (MCP) server för Pigello fastighetshanteringssystem. Denna server exponerar verktyg för att interagera med Pigello Mock API via MCP-protokollet.

## Funktioner

MCP servern tillhandahåller följande verktyg:

### Ärenden (Cases)
- `GetCases` - Hämta alla ärenden med valfri filtrering på användare, byggnad, fastighet eller status
- `GetCase` - Hämta ett specifikt ärende baserat på ID
- `UpdateCaseStatus` - Uppdatera status på ett ärende (stänga/avsluta)
- `CreateCase` - Skapa ett nytt ärende

### Byggnader och Rum
- `GetBuildings` - Hämta alla byggnader eller filtrera på fastighet
- `GetRooms` - Hämta alla rum eller filtrera på byggnad

### Komponenter
- `GetComponents` - Hämta komponenter med filtrering på rum, byggnad eller status
- `CreateComponent` - Skapa en ny komponent i ett rum
- `GetComponentModels` - Hämta komponentmodeller med filtrering på kategori
- `CreateComponentModel` - Skapa en ny komponentmodell

### Övriga
- `GetUsers` - Hämta alla användare
- `GetProperties` - Hämta alla fastigheter

## Konfiguration

### Lokalt (Development)
1. Se till att Pigello Mock API körs på `http://localhost:5059`
2. Kör MCP servern:
   ```bash
   dotnet run
   ```
3. MCP endpoint finns på: `http://localhost:5000/api/mcp`

### Azure App Service (Production)
1. Uppdatera `appsettings.Production.json` med rätt URL till Mock API:
   ```json
   {
     "PigelloMockAPI": {
       "BaseUrl": "https://your-mock-api.azurewebsites.net"
     }
   }
   ```

2. Deploy till Azure App Service:
   ```bash
   az webapp up --name pigello-mcp-server --resource-group your-rg
   ```

3. MCP endpoint: `https://pigello-mcp-server.azurewebsites.net/api/mcp`

## Endpoints

- `/` - Service information
- `/health` - Hälsokontroll för Azure
- `/api/mcp` - MCP endpoint (HTTP/SSE transport)

## Användning med GitHub Copilot

För att använda denna MCP server med GitHub Copilot i VS Code eller Codespaces, lägg till i din MCP-konfiguration:

```json
{
  "mcpServers": {
    "pigello": {
      "type": "sse",
      "url": "http://localhost:5000/api/mcp"
    }
  }
}
```

För Azure-hosted version:
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

## Exempel på användning

När MCP servern är konfigurerad i din chat-klient kan du ställa frågor som:

- "Visa mina ärenden" → Anropar `GetCases` med din användar-ID
- "Vilka ärenden finns för byggnad Rosenlund?" → Filtrerar på byggnad
- "Stäng ärende X" → Anropar `UpdateCaseStatus` med status=Closed
- "Skapa ett nytt ärende för VVS-problem" → Anropar `CreateCase`
- "Vilka komponenter finns i kök?" → Anropar `GetComponents` filtrerat på rum

## Teknisk Stack

- .NET 9.0
- ASP.NET Core Web API
- ModelContextProtocol.AspNetCore (Preview)
- HTTP/SSE transport för MCP
- Azure App Service ready

## Utveckling

```bash
# Återställ dependencies
dotnet restore

# Bygg projektet
dotnet build

# Kör lokalt
dotnet run

# Publicera för Azure
dotnet publish -c Release
```

## Miljövariabler för Azure

I Azure App Service, konfigurera följande Application Settings:

- `PigelloMockAPI__BaseUrl` - URL till Mock API (t.ex. `https://pigello-mock.azurewebsites.net`)

## Licens

Detta är en intern prototyp för Pigello fastighetshantering.
