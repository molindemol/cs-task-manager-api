# C# Task Manager API

A learning project to master C# and ASP.NET Core development. This repository implements a RESTful API for managing tasks with a PostgreSQL database.

## Learning Objectives

This project demonstrates:
- ASP.NET Core 10 Web API development
- Entity Framework Core with PostgreSQL
- Dependency Injection and service architecture
- RESTful API design principles
- Database migrations
- Swagger/OpenAPI documentation
- CRUD operations

## Tech Stack

- **Runtime**: .NET 10.0
- **Framework**: ASP.NET Core Web API
- **ORM**: Entity Framework Core 10.0
- **Database**: PostgreSQL
- **API Documentation**: Swagger/OpenAPI
- **NuGet Packages**:
  - Npgsql.EntityFrameworkCore.PostgreSQL
  - Swashbuckle.AspNetCore
  - Microsoft.AspNetCore.OpenApi

## Prerequisites

- .NET 10.0 SDK installed
- PostgreSQL database
- A code editor (VS Code, Visual Studio, etc.)

## Setup Instructions

### 1. Clone and Restore Dependencies

```bash
# Restore NuGet packages
dotnet restore
```

### 2. Configure Database Connection

Set up the PostgreSQL connection string using user-secrets:

```bash
dotnet user-secrets set "ConnectionStrings:Tasks" "Host=localhost;Port=5432;Database=taskmanagerdb;Username=<your_user>;Password=<your_password>"
```

### 3. Create and Apply Migrations

```bash
# Create a new migration
dotnet ef migrations add InitialCreate

# Apply migrations to database
dotnet ef database update
```

### 4. Run the API

```bash
dotnet run
```

The API will be available at `https://localhost:PORT`. The actual port number is displayed in the console output when the application starts. Check the `Properties/launchSettings.json` file to see the configured ports, or look for the message in the terminal showing which port the application is listening on.

Examples:
- Swagger UI: `https://localhost:<port>/swagger` (replace `<port>` with your actual port)
- API base: `https://localhost:<port>`

## API Endpoints

All endpoints use the `/task` base route.

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/task` | Get all tasks |
| GET | `/task/{id}` | Get a specific task by ID |
| POST | `/task` | Create a new task |
| PUT | `/task/{id}` | Update an existing task |
| DELETE | `/task/{id}` | Delete a task |

### Example Request

```json
POST /task
{
  "title": "Learn C#",
  "description": "Complete the task manager API project",
  "status": "In Progress"
}
```

### Task Model

```json
{
  "id": 1,
  "title": "string",
  "description": "string",
  "status": "string"
}
```

## Project Structure

```
taskManagerApi/
├── Controllers/       # API endpoints
│   └── TaskController.cs
├── Models/           # Data models and DTOs
│   └── Task.cs
├── Services/         # Business logic
│   └── TaskService.cs
├── Migrations/       # Database migrations
├── Properties/       # Launch settings
├── Program.cs        # Application entry point
├── taskManagerApi.csproj
└── README.md
```

## Key Concepts Covered

- **Dependency Injection**: TaskService injected into controller
- **Entity Framework Core**: DbContext setup for database operations
- **Data Transfer Objects (DTOs)**: UpdateTaskDto for update operations
- **RESTful API**: Standard HTTP methods for CRUD operations
- **Swagger Integration**: Auto-generated API documentation

## Development

### Run in Development Mode

```bash
dotnet run --environment Development
```

### View Swagger Documentation

Navigate to `https://localhost:<port>/swagger` to interact with the API (replace `<port>` with the actual port shown when you run the application).

### Testing the API

The repository includes `taskManagerApi.http` for testing endpoints with REST Client or similar tools.

## Notes

- Database connection string is stored using `dotnet user-secrets` for security
- Nullable reference types are enabled for better null safety
- Implicit usings are configured for cleaner code

## Resources Used for Learning

This project was built following these Microsoft Learn modules:

- [Write your first C# code](https://learn.microsoft.com/training/modules/csharp-write-first/)
- [Build web APIs with ASP.NET Core](https://learn.microsoft.com/training/modules/build-web-api-aspnet-core/)
- [Build a web API with minimal database - ASP.NET Core](https://learn.microsoft.com/training/modules/build-web-api-minimal-database/)

### Additional Resources

- [ASP.NET Core Documentation](https://docs.microsoft.com/aspnet/core)
- [Entity Framework Core](https://docs.microsoft.com/ef/core)
- [PostgreSQL Documentation](https://www.postgresql.org/docs)
- [RESTful API Design Best Practices](https://restfulapi.net)