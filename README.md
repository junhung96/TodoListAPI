# 🚀 Todo List API with JWT Authentication

A REST API for managing todo items with secure JWT authentication and comprehensive Swagger documentation.

## ✨ Features

- **JWT Authentication** - Secure endpoints with bearer tokens
- **Swagger UI** - Interactive API documentation
- **Clean Architecture** - Repository pattern with Dapper
- **Validation** - Robust input validation
- **Migrations** - EF Core database migrations
- **Response Wrapping** - Consistent API responses

### Prerequisites
- [.NET 8.0 SDK](https://dotnet.microsoft.com/download)
- [SQL Server 2019+](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
- [Visual Studio 2022](https://visualstudio.microsoft.com/)

### Installation

1. Clone the repository
   ```bash
   git clone https://github.com/junhung96/TodoListAPI.git
   cd TodoListAPI

2. Configure database connection
   Edit appsettings.json Connection string:
  "ConnectionStrings": {
    "DefaultConnection": "Server=yourlocalserver;Database=TodoDB;Trusted_Connection=True;TrustServerCertificate=True;"
  },

3. Apply database migrations
   ## Database Setup
  ### Using Package Manager Console (Visual Studio)
  1. Open **Package Manager Console**  
     (`Tools` > `NuGet Package Manager` > `Package Manager Console`)
  2. Run:
     ```powershell
     Update-Database
  3. Run the application
     ```bash
     dotnet run

### 🔐 Authentication
Default authentication test user:

Username: admin

Password: password
