# Station API Documentation

## Overview
The Station API is part of the EV Station-based Rental System. It provides comprehensive endpoints to manage EV charging stations, including location management, parking slot tracking, and operational hours.

## Technology Stack
- .NET 8.0
- Entity Framework Core 8.0
- SQL Server
- Swagger/OpenAPI

## Project Structure

```
StationAPI/
├── Controllers/
│   └── StationController.cs           # API endpoints
├── Data/
│   └── StationDbContext.cs            # Database context
├── DTOs/
│   ├── CreateStationRequestDto.cs     # Create request DTO
│   ├── UpdateStationRequestDto.cs     # Update request DTO
│   ├── StationResponseDto.cs          # Response DTO
│   └── ApiResponseDto.cs              # Standard API response
├── Models/
│   └── Station.cs                     # Station entity
├── Repository/
│   ├── IRepository/
│   │   └── IStationRepository.cs      # Repository interface
│   └── StationRepository.cs           # Repository implementation
├── Services/
│   ├── IService/
│   │   └── IStationService.cs         # Service interface
│   └── StationService.cs              # Service implementation
├── Migrations/                         # EF Core migrations
└── Program.cs                          # Application entry point
```

## Database Schema

### Stations Table
| Column            | Type           | Description                                    |
|-------------------|----------------|------------------------------------------------|
| StationId         | INT (PK)       | Primary key, auto-increment                    |
| StationName       | NVARCHAR(200)  | Name of the station                            |
| Address           | NVARCHAR(500)  | Full address                                   |
| City              | NVARCHAR(100)  | City name                                      |
| Province          | NVARCHAR(100)  | Province/State (optional)                      |
| PostalCode        | NVARCHAR(20)   | Postal/ZIP code (optional)                     |
| PhoneNumber       | NVARCHAR(20)   | Contact phone number (optional)                |
| Email             | NVARCHAR(100)  | Contact email (optional)                       |
| TotalParkingSlots | INT            | Total number of parking slots                  |
| AvailableSlots    | INT            | Currently available slots                      |
| Status            | NVARCHAR(50)   | Status: Active, Inactive, Under Maintenance    |
| Description       | NVARCHAR(1000) | Additional description (optional)              |
| Latitude          | FLOAT(10)      | GPS latitude coordinate (optional)             |
| Longitude         | FLOAT(10)      | GPS longitude coordinate (optional)            |
| OpeningTime       | TIME           | Daily opening time (optional)                  |
| ClosingTime       | TIME           | Daily closing time (optional)                  |
| IsOpen24Hours     | BIT            | True if station operates 24/7                  |
| CreatedAt         | DATETIME2      | Creation timestamp (UTC)                       |
| UpdatedAt         | DATETIME2      | Last update timestamp (UTC)                    |
| IsActive          | BIT            | Soft delete flag                               |

**Indexes:**
- IX_Stations_City
- IX_Stations_Status
- IX_Stations_StationName

## API Endpoints

### Base URL
```
https://localhost:{port}/api/station
```

### Endpoints Overview

#### 1. Create Station
**POST** `/api/station`

Creates a new station.

**Request Body:**
```json
{
  "stationName": "Downtown EV Station",
  "address": "123 Main Street",
  "city": "Ho Chi Minh City",
  "province": "Ho Chi Minh",
  "postalCode": "700000",
  "phoneNumber": "+84901234567",
  "email": "downtown@evstation.com",
  "totalParkingSlots": 50,
  "availableSlots": 50,
  "status": "Active",
  "description": "Large EV charging station in downtown area",
  "latitude": 10.762622,
  "longitude": 106.660172,
  "openingTime": "06:00:00",
  "closingTime": "22:00:00",
  "isOpen24Hours": false
}
```

**Response:** `200 OK`
```json
{
  "isSuccess": true,
  "message": "Station created successfully",
  "data": {
    "stationId": 1,
    "stationName": "Downtown EV Station",
    "address": "123 Main Street",
    "city": "Ho Chi Minh City",
    "province": "Ho Chi Minh",
    "postalCode": "700000",
    "phoneNumber": "+84901234567",
    "email": "downtown@evstation.com",
    "totalParkingSlots": 50,
    "availableSlots": 50,
    "status": "Active",
    "description": "Large EV charging station in downtown area",
    "latitude": 10.762622,
    "longitude": 106.660172,
    "openingTime": "06:00:00",
    "closingTime": "22:00:00",
    "isOpen24Hours": false,
    "createdAt": "2025-10-06T03:20:00Z",
    "updatedAt": null,
    "isActive": true
  }
}
```

---

#### 2. Get Station by ID
**GET** `/api/station/{stationId}`

Retrieves a specific station by ID.

**Response:** `200 OK` / `404 Not Found`

---

#### 3. Get All Stations
**GET** `/api/station`

Retrieves all active stations.

**Response:** `200 OK`

---

#### 4. Get Stations by City
**GET** `/api/station/city/{city}`

Retrieves all stations in a specific city.

**Example:**
```
GET /api/station/city/Ho Chi Minh City
```

---

#### 5. Get Stations by Status
**GET** `/api/station/status/{status}`

Retrieves all stations with a specific status.

**Example:**
```
GET /api/station/status/Active
```

**Valid Statuses:**
- Active
- Inactive
- Under Maintenance

---

#### 6. Update Station
**PUT** `/api/station/{stationId}`

Updates an existing station.

**Request Body:**
```json
{
  "stationName": "Downtown EV Station - Updated",
  "address": "123 Main Street, Updated Building",
  "city": "Ho Chi Minh City",
  "province": "Ho Chi Minh",
  "postalCode": "700000",
  "phoneNumber": "+84901234567",
  "email": "downtown@evstation.com",
  "totalParkingSlots": 60,
  "availableSlots": 55,
  "status": "Active",
  "description": "Expanded station with more slots",
  "latitude": 10.762622,
  "longitude": 106.660172,
  "openingTime": "05:00:00",
  "closingTime": "23:00:00",
  "isOpen24Hours": false
}
```

**Response:** `200 OK` / `404 Not Found`

---

#### 7. Delete Station
**DELETE** `/api/station/{stationId}`

Soft deletes a station (sets IsActive to false).

**Response:** `200 OK` / `404 Not Found`

---

#### 8. Get Stations with Available Slots
**GET** `/api/station/available-slots`

Retrieves all active stations that have available parking slots.

**Response:** `200 OK`

---

#### 9. Search Stations
**GET** `/api/station/search/{searchTerm}`

Searches stations by name, address, or city.

**Example:**
```
GET /api/station/search/Downtown
```

---

#### 10. Update Available Slots
**PATCH** `/api/station/{stationId}/available-slots/{availableSlots}`

Updates only the available slots for a station.

**Example:**
```
PATCH /api/station/1/available-slots/45
```

**Response:** `200 OK`
```json
{
  "isSuccess": true,
  "message": "Available slots updated successfully",
  "data": {
    "stationId": 1,
    "availableSlots": 45
  }
}
```

---

#### 11. Get Station Statistics
**GET** `/api/station/statistics`

Retrieves overall statistics about stations.

**Response:** `200 OK`
```json
{
  "isSuccess": true,
  "message": "Statistics retrieved successfully",
  "data": {
    "totalStations": 10,
    "activeStations": 8,
    "inactiveStations": 2
  }
}
```

---

## Setup Instructions

### Prerequisites
- .NET 8.0 SDK
- SQL Server (LocalDB or Express)
- Visual Studio 2022 or VS Code

### Installation

1. **Clone the repository**
   ```bash
   git clone <repository-url>
   cd EVStation-basedRendtalSystem.Services.StationAPI
   ```

2. **Update Connection String**
   
   Edit `appsettings.json` and update the connection string:
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=localhost;Database=EVRentalStationDB;Trusted_Connection=True;TrustServerCertificate=True"
     }
   }
   ```

3. **Restore NuGet Packages**
   ```bash
   dotnet restore
   ```

4. **Apply Migrations**
   ```bash
   dotnet ef database update
   ```

5. **Run the Application**
   ```bash
   dotnet run
   ```

6. **Access Swagger UI**
   
   Navigate to: `https://localhost:{port}/swagger`

---

## Migration Commands

### Create a new migration
```bash
dotnet ef migrations add MigrationName
```

### Apply migrations to database
```bash
dotnet ef database update
```

### Remove last migration
```bash
dotnet ef migrations remove
```

### Drop database
```bash
dotnet ef database drop
```

---

## Design Patterns Used

### Repository Pattern
Abstracts data access logic and provides a clean separation between business logic and data access layers.

### Service Layer Pattern
Contains business logic and orchestrates operations between controllers and repositories.

### Dependency Injection
All services and repositories are registered in `Program.cs` and injected via constructor injection.

---

## Validation Rules

### Required Fields
- StationName (max 200 chars)
- Address (max 500 chars)
- City (max 100 chars)

### Optional Fields
- Province (max 100 chars)
- PostalCode (max 20 chars)
- PhoneNumber (max 20 chars, must be valid format)
- Email (max 100 chars, must be valid format)
- Description (max 1000 chars)

### Numeric Validations
- TotalParkingSlots: 0-10000
- AvailableSlots: 0-10000 (cannot exceed TotalParkingSlots)
- Latitude: -90 to 90
- Longitude: -180 to 180

---

## Business Rules

1. **Unique Station Names**: Each station must have a unique name.
2. **Available Slots Validation**: Available slots cannot exceed total parking slots.
3. **Soft Delete**: Stations are never permanently deleted, only marked as inactive.
4. **UTC Timestamps**: All timestamps are stored in UTC.
5. **Auto Timestamps**: CreatedAt is automatically set on creation, UpdatedAt on modification.
6. **Default Status**: New stations default to "Active" status.

---

## Status Types

- **Active**: Station is operational and accepting rentals
- **Inactive**: Station is temporarily closed
- **Under Maintenance**: Station is undergoing maintenance

---

## Error Handling

All endpoints return a standard `ApiResponseDto` with:
- `isSuccess`: Boolean indicating success/failure
- `message`: Description of the result
- `data`: Response data (null on error)

Example error response:
```json
{
  "isSuccess": false,
  "message": "Station not found",
  "data": null
}
```

---

## Performance Optimizations

1. **Database Indexes**: Created on frequently queried columns (City, Status, StationName)
2. **Async Operations**: All database operations are asynchronous
3. **Soft Delete**: Improves performance by avoiding actual deletions
4. **LINQ Optimization**: Efficient query generation with Entity Framework
5. **GPS Precision**: Latitude/Longitude stored with appropriate precision (10,7)

---

## Integration Examples

### Checking Available Slots Before Booking
```csharp
// Get station with available slots
var station = await GetStationById(stationId);
if (station.AvailableSlots > 0)
{
    // Proceed with booking
    // After booking, update available slots
    await UpdateAvailableSlots(stationId, station.AvailableSlots - 1);
}
```

### Finding Nearest Stations (with GPS coordinates)
```csharp
// Get all stations
var stations = await GetAllStations();
// Calculate distance using Haversine formula
// Sort by distance and return nearest
```

---

## Future Enhancements

- [ ] Add authentication/authorization
- [ ] Implement pagination for list endpoints
- [ ] Add filtering and sorting options
- [ ] Real-time slot availability using SignalR
- [ ] Integration with mapping services (Google Maps API)
- [ ] Add station amenities (WiFi, Restrooms, Cafe, etc.)
- [ ] Implement station ratings and reviews
- [ ] Add charging port types and availability
- [ ] Implement pricing information per station
- [ ] Add station images/photos
- [ ] Implement geospatial queries for finding nearby stations
- [ ] Add operating schedule for each day of the week

---

## Testing Tips

### Sample Test Data
```json
{
  "stationName": "Test Station 1",
  "address": "123 Test Street",
  "city": "Hanoi",
  "province": "Hanoi",
  "totalParkingSlots": 20,
  "availableSlots": 20,
  "status": "Active",
  "latitude": 21.028511,
  "longitude": 105.804817,
  "isOpen24Hours": true
}
```

### Common Test Scenarios
1. Create multiple stations in different cities
2. Update available slots as cars are rented/returned
3. Search for stations in a specific city
4. Find stations with available slots
5. Change station status to "Under Maintenance"
6. Test GPS coordinates for mapping integration

---

## License
This project is part of the EV Station-based Rental System.

## Contact
For questions or support, please contact the development team.

