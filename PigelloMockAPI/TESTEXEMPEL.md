# Pigello Mock API - Testexempel

## Snabbstart

```bash
cd PigelloMockAPI
dotnet run
```

API:et startar på `http://localhost:5058`

## Testexempel för användningsfall

### 1. Visa ärenden tilldelade till mig

**Hämta ärenden för Anna Andersson:**
```bash
curl "http://localhost:5058/api/cases?assignedToUserId=11111111-1111-1111-1111-111111111111"
```

**Hämta ärenden för Erik Eriksson:**
```bash
curl "http://localhost:5058/api/cases?assignedToUserId=22222222-2222-2222-2222-222222222222"
```

### 2. Visa ärenden för en byggnad

```bash
# Ärenden för Rosenlund Hus A
curl "http://localhost:5058/api/cases?buildingId=55555555-5555-5555-5555-555555555555"
```

### 3. Visa ärenden för en fastighet

```bash
# Ärenden för fastighet Rosenlund
curl "http://localhost:5058/api/cases?propertyId=33333333-3333-3333-3333-333333333333"
```

### 4. Avsluta ett ärende

```bash
# Stäng ärende (status 3 = Closed)
curl -X PATCH "http://localhost:5058/api/cases/c1111111-1111-1111-1111-111111111111/status" \
  -H "Content-Type: application/json" \
  -d '3'
```

**Status-värden:**
- 0 = Open
- 1 = InProgress
- 2 = Pending
- 3 = Closed
- 4 = Cancelled

### 5. Skapa en komponentmodell

```bash
curl -X POST "http://localhost:5058/api/componentmodels" \
  -H "Content-Type: application/json" \
  -d '{
    "name": "Bosch Diskmaskin SMV46KX01E",
    "category": "Vitvaror",
    "manufacturer": "Bosch",
    "modelNumber": "SMV46KX01E",
    "expectedLifespanMonths": 144,
    "description": "Integrerad diskmaskin",
    "specifications": {
      "energyClass": "A++",
      "capacity": "13 kuvert",
      "noiseLevel": "46 dB"
    }
  }'
```

**Spara komponenten-ID från responsen för nästa steg!**

### 6. Skapa en komponent i ett rum

```bash
# Först, hämta tillgängliga rum
curl "http://localhost:5058/api/rooms"

# Sedan, skapa komponenten
curl -X POST "http://localhost:5058/api/components" \
  -H "Content-Type: application/json" \
  -d '{
    "roomId": "77777777-7777-7777-7777-777777777777",
    "componentModelId": "aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa",
    "installationDate": "2024-10-27",
    "serialNumber": "SN987654321",
    "status": 0,
    "warrantyExpirationDate": "2027-10-27",
    "notes": "Installerad vid renovering av kök"
  }'
```

**Component Status-värden:**
- 0 = Active
- 1 = Inactive
- 2 = Maintenance
- 3 = Defect
- 4 = Replaced

## Användbara kommandon

### Lista alla användare
```bash
curl "http://localhost:5058/api/users"
```

### Lista alla fastigheter
```bash
curl "http://localhost:5058/api/properties"
```

### Lista alla byggnader
```bash
curl "http://localhost:5058/api/buildings"
```

### Lista rum i en byggnad
```bash
curl "http://localhost:5058/api/buildings/55555555-5555-5555-5555-555555555555/rooms"
```

### Lista alla komponenter i ett rum
```bash
curl "http://localhost:5058/api/components?roomId=77777777-7777-7777-7777-777777777777"
```

### Lista alla komponentmodeller
```bash
curl "http://localhost:5058/api/componentmodels"
```

### Hämta specifik komponentmodell
```bash
curl "http://localhost:5058/api/componentmodels/aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"
```

## Fördefinierade test-ID:n

### Användare
- **Anna Andersson** (Fastighetsförvaltare): `11111111-1111-1111-1111-111111111111`
- **Erik Eriksson** (Tekniker): `22222222-2222-2222-2222-222222222222`

### Fastigheter
- **Rosenlund**: `33333333-3333-3333-3333-333333333333`
- **Liljeholm**: `44444444-4444-4444-4444-444444444444`

### Byggnader
- **Rosenlund Hus A**: `55555555-5555-5555-5555-555555555555`
- **Rosenlund Hus B**: `66666666-6666-6666-6666-666666666666`

### Rum
- **Kök (lägenhet 1101)**: `77777777-7777-7777-7777-777777777777`
- **Badrum (lägenhet 1101)**: `88888888-8888-8888-8888-888888888888`
- **Tvättstuga (gemensam)**: `99999999-9999-9999-9999-999999999999`

### Komponentmodeller
- **Electrolux Kylskåp**: `aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa`
- **FM Mattsson Blandare**: `bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb`
- **Miele Tvättmaskin**: `cccccccc-cccc-cccc-cccc-cccccccccccc`

### Ärenden
- **Vattenläcka i tak**: `c1111111-1111-1111-1111-111111111111`
- **Kylskåp fungerar inte**: `c2222222-2222-2222-2222-222222222222`
- **Trasig tvättmaskin**: `c3333333-3333-3333-3333-333333333333`
- **Besiktning av lägenhet** (Closed): `c4444444-4444-4444-4444-444444444444`

## Tips för testning

### Formatera JSON-output med Python
```bash
curl "http://localhost:5058/api/cases" | python3 -m json.tool
```

### Filtrera öppna ärenden
```bash
curl "http://localhost:5058/api/cases?status=0"
```

### Kombinera filter
```bash
curl "http://localhost:5058/api/cases?assignedToUserId=22222222-2222-2222-2222-222222222222&status=0"
```
