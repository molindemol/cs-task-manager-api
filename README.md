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
- Entity relationships (one-to-many)
- Data Transfer Objects (DTOs) to prevent circular references
- API response mapping and transformation

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

### Board Endpoints

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/board` | Get all boards |
| GET | `/board/{id}` | Get a specific board with columns and tasks |
| POST | `/board` | Create a new board |
| PUT | `/board/{id}` | Update a board |
| DELETE | `/board/{id}` | Delete a board |

### Column Endpoints

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/column` | Get all columns |
| GET | `/column/{id}` | Get a specific column with tasks |
| GET | `/column/board/{boardId}` | Get all columns for a specific board |
| POST | `/column` | Create a new column |
| PUT | `/column/{id}` | Update a column |
| DELETE | `/column/{id}` | Delete a column |

### Task Endpoints

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/task` | Get all tasks |
| GET | `/task/{id}` | Get a specific task by ID |
| POST | `/task` | Create a new task |
| PUT | `/task/{id}` | Update an existing task |
| DELETE | `/task/{id}` | Delete a task |

### Example Requests

Create a Board:
```json
POST /board
{
  "name": "My Project",
  "description": "Project board for organizing tasks"
}
```

Create a Column:
```json
POST /column
{
  "name": "To Do",
  "position": 0,
  "boardId": 1
}
```

Create a Task:
```json
POST /task
{
  "title": "Learn C#",
  "description": "Complete the task manager API project",
  "status": "In Progress",
  "columnId": 1
}
```

### Data Models

**Board:**
```json
{
  "id": 1,
  "name": "string",
  "description": "string",
  "createdAt": "datetime",
  "columns": [...]
}
```

**Column:**
```json
{
  "id": 1,
  "name": "string",
  "position": 0,
  "boardId": 1,
  "tasks": [...]
}
```

**Task:**
```json
{
  "id": 1,
  "title": "string",
  "description": "string",
  "status": "string",
  "columnId": 1
}
```

## Data Model Relationships

The API implements a hierarchical structure:

- Each **Board** can contain multiple **Columns**
- Each **Column** can contain multiple **Tasks**
- Tasks are organized within columns for better workflow management
- Columns have a **Position** property to maintain ordering on the board

## Project Structure

```
taskManagerApi/
├── Controllers/              # API endpoints
│   ├── TaskController.cs
│   ├── BoardController.cs
│   └── ColumnController.cs
├── Models/                   # Data models and DTOs
│   ├── Task.cs
│   ├── Board.cs
│   ├── Column.cs
│   └── DTOs.cs
├── Services/                 # Business logic
│   ├── TaskService.cs
│   ├── BoardService.cs
│   └── ColumnService.cs
├── Migrations/               # Database migrations
├── Properties/               # Launch settings
├── Program.cs                # Application entry point
├── taskManagerApi.csproj
└── README.md
```

## Key Concepts Covered

- **Dependency Injection**: Services injected into controllers
- **Entity Framework Core**: DbContext setup with relationships
- **Data Transfer Objects (DTOs)**: Separating API responses from database models to prevent circular references
- **Entity Relationships**: One-to-many relationships (Board → Columns → Tasks)
- **RESTful API**: Standard HTTP methods for CRUD operations
- **Swagger Integration**: Auto-generated API documentation
- **Database Migrations**: Managing schema changes with EF Core

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