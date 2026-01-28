# Replate Backend API

## 📋 Overview

**Replate** is a food deals platform that connects vendors (restaurants, bakeries, grocery stores) with customers looking for discounted food items. The platform helps reduce food waste by allowing vendors to sell surplus food at discounted prices.

### Key Features
- **Vendor Management**: Vendors can create profiles and manage their business information
- **Deal Management**: Vendors can post food deals with discounts
- **Reservations**: Customers can reserve deals and pick them up at specified times
- **User Authentication**: Firebase-based authentication (Customer, Vendor, Admin roles)

---

## 🏗️ Architecture

This project follows **Clean Architecture** principles with **CQRS pattern** using MediatR.

```
┌─────────────────────────────────────────────────────────────┐
│                      Replate.Api                            │
│                   (Presentation Layer)                      │
│              Controllers, Middleware, Startup               │
├─────────────────────────────────────────────────────────────┤
│                  Replate.Application                        │
│                   (Application Layer)                       │
│        Commands, Queries, DTOs, Validators, Mappings        │
├─────────────────────────────────────────────────────────────┤
│                  Replate.Infrastructure                     │
│                 (Infrastructure Layer)                      │
│          DbContext, Configurations, Migrations, Seeds       │
├─────────────────────────────────────────────────────────────┤
│                    Replate.Domain                           │
│                    (Domain Layer)                           │
│                 Entities, Enums, Interfaces                 │
└─────────────────────────────────────────────────────────────┘
```

### Project Structure

```
backend/
├── Replate.Api/                    # Web API (Entry Point)
│   ├── Controllers/                # API Controllers
│   ├── Properties/                 # Launch settings
│   ├── Program.cs                  # Application startup
│   └── appsettings.json           # Configuration
│
├── Replate.Application/            # Business Logic
│   ├── Common/                     # Shared models (Result<T>)
│   ├── Features/                   # Feature-based organization
│   │   ├── VendorProfiles/        # Vendor profile feature
│   │   │   ├── Commands/          # Create, Update operations
│   │   │   ├── Queries/           # Read operations
│   │   │   ├── DTOs/              # Data Transfer Objects
│   │   │   └── MappingProfile.cs  # AutoMapper configuration
│   │   └── Deal/                  # Deal feature (TODO)
│   ├── Interface/                  # Abstractions
│   └── DependencyInjection.cs     # Service registration
│
├── Replate.Infrastructure/         # Data Access & External Services
│   ├── Persistence/
│   │   ├── Configurations/        # EF Core entity configurations
│   │   ├── Migrations/            # Database migrations
│   │   ├── Seeds/                 # Seed data
│   │   ├── ReplateDbContext.cs    # DbContext
│   │   └── ReplateDbContextFactory.cs # Design-time factory
│   └── DependencyInjection.cs     # Service registration
│
├── Replate.Domain/                 # Core Domain
│   ├── Entities/                   # Domain entities
│   └── Enums/                      # Enumerations
│
└── docs/                           # Documentation
```

---

## 🛠️ Technology Stack

| Technology | Purpose |
|------------|---------|
| **.NET 10** | Runtime framework |
| **ASP.NET Core** | Web API framework |
| **Entity Framework Core 10** | ORM / Data access |
| **SQL Server (LocalDB)** | Database |
| **MediatR** | CQRS / Mediator pattern |
| **AutoMapper** | Object mapping |
| **FluentValidation** | Input validation |
| **Swashbuckle** | Swagger / OpenAPI documentation |
| **Firebase Admin** | Authentication (planned) |

---

## 🚀 Getting Started

### Prerequisites
- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- [SQL Server LocalDB](https://docs.microsoft.com/en-us/sql/database-engine/configure-windows/sql-server-express-localdb) (comes with Visual Studio)
- [JetBrains Rider](https://www.jetbrains.com/rider/) or Visual Studio 2022+

### Setup Steps

1. **Clone the repository**
   ```bash
   git clone <repository-url>
   cd replate/backend
   ```

2. **Restore dependencies**
   ```bash
   dotnet restore
   ```

3. **Update database** (applies migrations)
   ```bash
   cd Replate.Api
   dotnet ef database update --project ../Replate.Infrastructure
   ```

4. **Run the application**
   ```bash
   dotnet run --project Replate.Api
   ```

5. **Access Swagger UI**
   - Open browser: `https://localhost:5001` or `http://localhost:5000`

### Connection Strings

**Development** (`appsettings.Development.json`):
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=ReplateDb_Dev;MultipleActiveResultSets=true"
  }
}
```

**Production** (`appsettings.json`):
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=ReplateDb;Trusted_Connection=True;MultipleActiveResultSets=true"
  }
}
```

---

## 📊 Database Schema

See [DATABASE.md](./DATABASE.md) for detailed entity documentation.

---

## 🔧 Development Guidelines

See [DEVELOPMENT.md](./DEVELOPMENT.md) for coding standards and patterns.

---

## 📡 API Reference

See [API.md](./API.md) for endpoint documentation.

---

## 🧪 Testing

```bash
# Run all tests
dotnet test

# Run with coverage
dotnet test --collect:"XPlat Code Coverage"
```

---

## 📝 Contributing

1. Create a feature branch from `main`
2. Follow the coding standards in [DEVELOPMENT.md](./DEVELOPMENT.md)
3. Write tests for new features
4. Submit a pull request

---

## 📞 Support

For questions or issues, contact the development team.
