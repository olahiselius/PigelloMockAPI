# Tenant (Hyresgäst) API

## Översikt
Tenant-funktionaliteten hanterar hyresgäster i Pigello Mock API-systemet. En hyresgäst kan vara antingen en privatperson eller ett företag som hyr fastigheter, lägenheter eller lokaler.

## Modell

### Tenant-attribut
| Fält | Typ | Beskrivning |
|------|-----|-------------|
| `id` | Guid | Unikt ID för hyresgästen |
| `firstName` | string | Förnamn (för privatpersoner) |
| `lastName` | string | Efternamn (för privatpersoner) |
| `fullName` | string | Fullständigt namn (läsbart) |
| `ssn` | string | Personnummer (format: YYYYMMDD-XXXX) |
| `email` | string | E-postadress |
| `phoneNumber` | string | Telefonnummer |
| `birthDate` | DateTime? | Födelsedatum |
| `isActive` | bool | Om hyresgästen är aktiv |
| `activeFrom` | DateTime? | Datum då hyresgästen blir aktiv (från) |
| `activeTo` | DateTime? | Datum då hyresgästen slutar vara aktiv (till) |
| `organizationId` | Guid? | Organisation som hyresgästen tillhör |
| `createdAt` | DateTime | Skapad tidpunkt |
| `lastLogin` | DateTime? | Senaste inloggning |
| `corporateName` | string? | Företagsnamn (för företagskunder) |
| `orgNo` | string? | Organisationsnummer (för företagskunder) |
| `isCompany` | bool | Om detta är ett företag |
| `invoiceAddress` | string? | Faktureringsadress |
| `invoiceEmail` | string? | E-post för fakturor |
| `notes` | string? | Anteckningar/noteringar |

## API Endpoints

### GET /api/tenants
Hämta alla hyresgäster med valfri filtrering.

**Query-parametrar:**
- `isActive` (bool, optional): Filtrera på aktiva/inaktiva hyresgäster
- `isCompany` (bool, optional): Filtrera på företag/privatpersoner
- `organizationId` (Guid, optional): Filtrera på organisation

**Exempel:**
```bash
# Hämta alla aktiva hyresgäster
GET /api/tenants?isActive=true

# Hämta alla privatpersoner
GET /api/tenants?isCompany=false

# Hämta alla företagshyresgäster i en organisation
GET /api/tenants?isCompany=true&organizationId=e5f6a7b8-c9d1-4e5f-3a4b-5c6d7e8f9a1b
```

### GET /api/tenants/{id}
Hämta en specifik hyresgäst baserat på ID.

**Exempel:**
```bash
GET /api/tenants/a1b2c3d4-e5f6-4a5b-8c9d-1e2f3a4b5c6d
```

### POST /api/tenants
Skapa en ny hyresgäst.

**Request body (privatperson):**
```json
{
  "firstName": "Anna",
  "lastName": "Andersson",
  "ssn": "19850315-2468",
  "email": "anna.andersson@example.com",
  "phoneNumber": "+46701234567",
  "birthDate": "1985-03-15",
  "isActive": true,
  "isCompany": false,
  "invoiceAddress": "Rosenlundsvägen 1, 118 20 Stockholm",
  "invoiceEmail": "anna.andersson@example.com",
  "notes": "Ny hyresgäst"
}
```

**Request body (företag):**
```json
{
  "corporateName": "TechStartup AB",
  "orgNo": "556789-1234",
  "email": "kontakt@techstartup.se",
  "phoneNumber": "+46704567890",
  "isActive": true,
  "isCompany": true,
  "invoiceAddress": "Götgatan 45, 116 21 Stockholm",
  "invoiceEmail": "faktura@techstartup.se",
  "organizationId": "e5f6a7b8-c9d1-4e5f-3a4b-5c6d7e8f9a1b",
  "notes": "Kontorshyresgäst"
}
```

### PUT /api/tenants/{id}
Uppdatera en befintlig hyresgäst (fullständig uppdatering).

**Exempel:**
```bash
PUT /api/tenants/a1b2c3d4-e5f6-4a5b-8c9d-1e2f3a4b5c6d
Content-Type: application/json

{
  "firstName": "Anna",
  "lastName": "Andersson",
  "ssn": "19850315-2468",
  "email": "anna.ny@example.com",
  "phoneNumber": "+46709999999",
  "birthDate": "1985-03-15",
  "isActive": true,
  "isCompany": false,
  "invoiceEmail": "anna.ny@example.com"
}
```

### PATCH /api/tenants/{id}
Partiell uppdatering av en hyresgäst.

**Exempel:**
```bash
PATCH /api/tenants/a1b2c3d4-e5f6-4a5b-8c9d-1e2f3a4b5c6d
Content-Type: application/json

{
  "email": "anna.ny@example.com",
  "isActive": false
}
```

### DELETE /api/tenants/{id}
Ta bort en hyresgäst.

**Exempel:**
```bash
DELETE /api/tenants/a1b2c3d4-e5f6-4a5b-8c9d-1e2f3a4b5c6d
```

## MCP Tools

### GetTenants
Hämta alla hyresgäster eller filtrera på aktiva/inaktiva, företag/privatpersoner eller organisation.

**Parametrar:**
- `isActive` (bool, optional): Filtrera på aktiva/inaktiva hyresgäster
- `isCompany` (bool, optional): Filtrera på företag (true) eller privatpersoner (false)
- `organizationId` (string, optional): GUID för organisation

### GetTenant
Hämta en specifik hyresgäst baserat på ID.

**Parametrar:**
- `tenantId` (string): GUID för hyresgästen

### CreateTenant
Skapa en ny hyresgäst (privatperson).

**Parametrar:**
- `firstName` (string): Förnamn
- `lastName` (string): Efternamn
- `ssn` (string): Personnummer (format: YYYYMMDD-XXXX)
- `email` (string): E-postadress
- `phoneNumber` (string): Telefonnummer
- `birthDate` (string): Födelsedatum (ISO format: yyyy-MM-dd)
- `isActive` (bool, optional): Om hyresgästen är aktiv (standard: true)
- `invoiceAddress` (string, optional): Faktureringsadress
- `invoiceEmail` (string, optional): E-post för fakturor
- `notes` (string, optional): Anteckningar

### CreateCompanyTenant
Skapa en ny företagshyresgäst.

**Parametrar:**
- `corporateName` (string): Företagsnamn
- `orgNo` (string): Organisationsnummer (format: XXXXXX-XXXX)
- `email` (string): E-postadress
- `phoneNumber` (string): Telefonnummer
- `isActive` (bool, optional): Om hyresgästen är aktiv (standard: true)
- `invoiceAddress` (string, optional): Faktureringsadress
- `invoiceEmail` (string, optional): E-post för fakturor
- `organizationId` (string, optional): Organisations-ID
- `notes` (string, optional): Anteckningar

### UpdateTenant
Uppdatera en hyresgästs information.

**Parametrar:**
- `tenantId` (string): GUID för hyresgästen
- `tenantData` (string): JSON-data med uppdaterad information

## Testdata
Systemet innehåller 10 förinställda hyresgäster i seed-data:

### Privatpersoner (7 st)
1. **Anna Andersson** - Aktiv sedan 2020
2. **Erik Eriksson** - Aktiv sedan 2021, familj med barn
3. **Maria Johansson** - Aktiv sedan 2019, långvarig hyresgäst
4. **Lars Larsson** - Aktiv sedan 2018, pensionär
5. **Sofia Svensson** - Inaktiv (flyttade ut april 2024)
6. **Karin Nilsson** - Aktiv sedan 2023, student
7. **Peter Pettersson** - Aktiv sedan 2021, distansarbetare
8. **Emma Bergström** - Aktiv sedan 2022, har husdjur

### Företag (2 st)
1. **TechStartup AB** - Kontorshyresgäst, 3-årigt kontrakt
2. **Restaurang Gourmet AB** - Restauranglokal i bottenvåning, 5-årigt kontrakt

Alla GUID:er i testdatat är riktiga, korrekt genererade UUID:er.

## Användningsexempel

### Hitta alla aktiva privatpersoner
```bash
GET /api/tenants?isActive=true&isCompany=false
```

### Hitta alla företagshyresgäster
```bash
GET /api/tenants?isCompany=true
```

### Skapa en ny privatperson som hyresgäst
```bash
POST /api/tenants
Content-Type: application/json

{
  "firstName": "Johan",
  "lastName": "Johansson",
  "ssn": "19900515-1234",
  "email": "johan@example.com",
  "phoneNumber": "+46701111111",
  "birthDate": "1990-05-15",
  "isActive": true,
  "isCompany": false
}
```

### Avsluta en hyresgästs hyresavtal
```bash
PATCH /api/tenants/{id}
Content-Type: application/json

{
  "isActive": false,
  "activeTo": "2024-12-31"
}
```
