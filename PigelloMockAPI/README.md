# Pigello Mock API

Ett mock API som simulerar Pigellos API för utveckling av klienter och MCP-servrar.

## Översikt

Detta API simulerar funktionalitet från Pigello, ett fastighetssystem som används av bostadsbolag i Sverige. API:et tillhandahåller endpoints för att hantera ärenden, byggnader, fastigheter, rum, komponenter och användare.

## Starta API:et

```bash
cd PigelloMockAPI
dotnet run
```

API:et startar på `http://localhost:5059` (eller annan port som visas i konsolen).

## Swagger/OpenAPI UI

Swagger UI är tillgängligt på root-adressen när API:et körs i Development-läge:

**Swagger UI:** `http://localhost:5059/`

Här kan du:
- 📖 Utforska alla tillgängliga endpoints
- 🧪 Testa API:et direkt från webbläsaren
- 📝 Se detaljerad dokumentation för varje endpoint
- 🔍 Inspektera request/response-modeller

**OpenAPI specification:** `http://localhost:5059/swagger/v1/swagger.json`

## Endpoints

### Användningsfall

#### 1. Vilka ärenden är tilldelade mig?
```http
GET /api/cases?assignedToUserId={userId}
```

**Exempel:**
```bash
curl https://localhost:5001/api/cases?assignedToUserId=11111111-1111-1111-1111-111111111111
```

#### 2. Vilka ärenden finns för byggnad X eller fastighet Y?
```http
GET /api/cases?buildingId={buildingId}
GET /api/cases?propertyId={propertyId}
```

**Exempel:**
```bash
# Ärenden för en specifik byggnad
curl https://localhost:5001/api/cases?buildingId=55555555-5555-5555-5555-555555555555

# Ärenden för en specifik fastighet
curl https://localhost:5001/api/cases?propertyId=33333333-3333-3333-3333-333333333333
```

#### 3. Avsluta ärende
```http
PATCH /api/cases/{caseId}/status
Content-Type: application/json

"Closed"
```

**Exempel:**
```bash
curl -X PATCH https://localhost:5001/api/cases/c1111111-1111-1111-1111-111111111111/status \
  -H "Content-Type: application/json" \
  -d '"Closed"'
```

#### 4. Skapa komponentmodell
```http
POST /api/componentmodels
Content-Type: application/json

{
  "name": "Kylskåp Electrolux ABC-123",
  "category": "Vitvaror",
  "manufacturer": "Electrolux",
  "modelNumber": "ABC-123",
  "expectedLifespanMonths": 120,
  "description": "Energieffektivt kylskåp",
  "specifications": {
    "energyClass": "A++",
    "capacity": "250L"
  }
}
```

**Exempel:**
```bash
curl -X POST https://localhost:5001/api/componentmodels \
  -H "Content-Type: application/json" \
  -d '{
    "name": "Kylskåp Electrolux ABC-123",
    "category": "Vitvaror",
    "manufacturer": "Electrolux",
    "modelNumber": "ABC-123",
    "expectedLifespanMonths": 120,
    "description": "Energieffektivt kylskåp",
    "specifications": {
      "energyClass": "A++",
      "capacity": "250L"
    }
  }'
```

#### 5. Skapa komponent i ett rum
```http
POST /api/components
Content-Type: application/json

{
  "roomId": "77777777-7777-7777-7777-777777777777",
  "componentModelId": "aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa",
  "installationDate": "2024-01-15",
  "serialNumber": "SN123456789",
  "status": "Active",
  "warrantyExpirationDate": "2026-01-15",
  "notes": "Installerad vid renovering"
}
```

**Exempel:**
```bash
curl -X POST https://localhost:5001/api/components \
  -H "Content-Type: application/json" \
  -d '{
    "roomId": "77777777-7777-7777-7777-777777777777",
    "componentModelId": "aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa",
    "installationDate": "2024-01-15",
    "serialNumber": "SN123456789",
    "status": "Active",
    "warrantyExpirationDate": "2026-01-15",
    "notes": "Installerad vid renovering"
  }'
```

### Alla Endpoints

#### Cases (Ärenden)
- `GET /api/cases` - Hämta alla ärenden (med filter)
- `GET /api/cases/{id}` - Hämta specifikt ärende
- `POST /api/cases` - Skapa nytt ärende
- `PUT /api/cases/{id}` - Uppdatera ärende
- `PATCH /api/cases/{id}/status` - Uppdatera ärendestatus
- `DELETE /api/cases/{id}` - Ta bort ärende

#### Buildings (Byggnader)
- `GET /api/buildings` - Hämta alla byggnader
- `GET /api/buildings/{id}` - Hämta specifik byggnad
- `GET /api/buildings/{id}/rooms` - Hämta rum i byggnad
- `POST /api/buildings` - Skapa ny byggnad
- `PUT /api/buildings/{id}` - Uppdatera byggnad
- `DELETE /api/buildings/{id}` - Ta bort byggnad

#### Properties (Fastigheter)
- `GET /api/properties` - Hämta alla fastigheter
- `GET /api/properties/{id}` - Hämta specifik fastighet
- `POST /api/properties` - Skapa ny fastighet
- `PUT /api/properties/{id}` - Uppdatera fastighet
- `DELETE /api/properties/{id}` - Ta bort fastighet

#### Rooms (Rum)
- `GET /api/rooms` - Hämta alla rum (med filter)
- `GET /api/rooms/{id}` - Hämta specifikt rum
- `POST /api/rooms` - Skapa nytt rum
- `PUT /api/rooms/{id}` - Uppdatera rum
- `DELETE /api/rooms/{id}` - Ta bort rum

#### Components (Komponenter)
- `GET /api/components` - Hämta alla komponenter (med filter)
- `GET /api/components/{id}` - Hämta specifik komponent
- `POST /api/components` - Skapa ny komponent
- `PUT /api/components/{id}` - Uppdatera komponent
- `DELETE /api/components/{id}` - Ta bort komponent

#### ComponentModels (Komponentmodeller)
- `GET /api/componentmodels` - Hämta alla komponentmodeller
- `GET /api/componentmodels/{id}` - Hämta specifik komponentmodell
- `POST /api/componentmodels` - Skapa ny komponentmodell
- `PUT /api/componentmodels/{id}` - Uppdatera komponentmodell
- `DELETE /api/componentmodels/{id}` - Ta bort komponentmodell

#### Users (Användare)
- `GET /api/users` - Hämta alla användare
- `GET /api/users/{id}` - Hämta specifik användare
- `GET /api/users/me` - Hämta inloggad användare (mock)
- `POST /api/users` - Skapa ny användare
- `PUT /api/users/{id}` - Uppdatera användare
- `DELETE /api/users/{id}` - Ta bort användare

## Mock Data

API:et innehåller fördefinierad mock-data:
- 2 användare (Anna Andersson, Erik Eriksson)
- 2 fastigheter (Rosenlund, Liljeholm)
- 2 byggnader (Rosenlund Hus A & B)
- 3 rum (Kök, Badrum, Tvättstuga)
- 3 komponentmodeller (Kylskåp, Blandare, Tvättmaskin)
- 3 komponenter
- 4 ärenden (olika status)

### Viktiga ID:n för tester

**Användare:**
- Anna Andersson: `11111111-1111-1111-1111-111111111111`
- Erik Eriksson: `22222222-2222-2222-2222-222222222222`

**Fastigheter:**
- Rosenlund: `33333333-3333-3333-3333-333333333333`

**Byggnader:**
- Rosenlund Hus A: `55555555-5555-5555-5555-555555555555`

**Rum:**
- Kök: `77777777-7777-7777-7777-777777777777`
- Tvättstuga: `99999999-9999-9999-9999-999999999999`

**Komponentmodeller:**
- Electrolux Kylskåp: `aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa`

## In-Memory Databas

API:et använder en in-memory databas som innebär:
- ✅ Data persisteras under applikationens livstid
- ✅ Du kan skapa, uppdatera och ta bort data via API:et
- ❌ Data återställs till ursprungliga mock-data vid omstart

## Nästa Steg

Nu när mock API:et är klart kan du utveckla en MCP-server som anropar dessa endpoints för att integrera med Pigello-systemet.

## Teknisk Stack

- .NET 9.0
- ASP.NET Core Web API
- In-Memory Data Store
- OpenAPI/Swagger
