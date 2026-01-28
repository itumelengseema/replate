# Onboarding Checklist for New Developers

Welcome to the Replate project! This checklist will help you get up to speed quickly.

---

## 📋 Day 1: Environment Setup

### Prerequisites
- [ ] Install [.NET 10 SDK](https://dotnet.microsoft.com/download)
- [ ] Install [JetBrains Rider](https://www.jetbrains.com/rider/) or Visual Studio 2022+
- [ ] Install [SQL Server Management Studio (SSMS)](https://docs.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms) or Azure Data Studio
- [ ] Install [Git](https://git-scm.com/)

### Project Setup
- [ ] Clone the repository
- [ ] Open solution in Rider: `backend/Replate.slnx`
- [ ] Restore NuGet packages: `dotnet restore`
- [ ] Build the solution: `dotnet build`
- [ ] Run the application: `dotnet run --project Replate.Api`
- [ ] Verify Swagger UI loads at `https://localhost:5001`

### Database Setup
- [ ] Verify LocalDB is running
- [ ] Run migrations: The app applies them automatically on startup
- [ ] Verify seed data in database (check Users, VendorProfiles tables)

---

## 📚 Day 1-2: Documentation Review

### Read These Documents (in order)
- [ ] [README.md](./README.md) - Project overview and architecture
- [ ] [DATABASE.md](./DATABASE.md) - Entity relationships and schema
- [ ] [DEVELOPMENT.md](./DEVELOPMENT.md) - Coding standards and patterns
- [ ] [API.md](./API.md) - API endpoints reference

### Understand the Architecture
- [ ] Understand Clean Architecture layers (Domain → Application → Infrastructure → Api)
- [ ] Understand CQRS pattern (Commands vs Queries)
- [ ] Understand MediatR request/handler pattern
- [ ] Understand Result<T> pattern for operation outcomes

---

## 🔍 Day 2-3: Code Exploration

### Domain Layer (`Replate.Domain`)
- [ ] Review all entities in `Entities/` folder
- [ ] Review all enums in `Enums/` folder
- [ ] Understand the relationships between entities

### Application Layer (`Replate.Application`)
- [ ] Review `DependencyInjection.cs` - how services are registered
- [ ] Review `Common/Models/Result.cs` - operation result pattern
- [ ] Review `Interface/IApplicationDbContext.cs` - database abstraction

### VendorProfiles Feature (Reference Implementation)
- [ ] Study `Features/VendorProfiles/` structure
- [ ] Review `Commands/CreateVendorProfile/` - full command implementation
- [ ] Review `Queries/GetVendorProfile/` - query implementation
- [ ] Review `DTOs/` - data transfer objects
- [ ] Review `MappingProfile.cs` - AutoMapper configuration

### Infrastructure Layer (`Replate.Infrastructure`)
- [ ] Review `ReplateDbContext.cs` - EF Core context
- [ ] Review `Configurations/` - entity configurations
- [ ] Review `Seeds/ApplicationDbContextSeed.cs` - seed data
- [ ] Review `DependencyInjection.cs` - infrastructure services

### API Layer (`Replate.Api`)
- [ ] Review `Program.cs` - application startup and middleware
- [ ] Review `Controllers/VendorProfilesController.cs` - API endpoints

---

## 🧪 Day 3-4: Hands-on Practice

### Test Existing Endpoints
- [ ] Use Swagger UI to test all VendorProfile endpoints
- [ ] Create a new vendor profile via API
- [ ] Get the created profile by publicId
- [ ] Update the profile
- [ ] Observe the request/response patterns

### Debug the Code
- [ ] Set breakpoint in `CreateVendorProfileCommandHandler.Handle()`
- [ ] Step through the code to understand the flow
- [ ] Observe how MediatR dispatches to handlers
- [ ] Observe how AutoMapper maps objects

### Review Database
- [ ] Connect to `ReplateDb_Dev` using SSMS
- [ ] Explore the table structure
- [ ] Query the data you created via API
- [ ] Understand the relationship between tables

---

## 🛠️ Day 4-5: First Task

### Create Your First Feature (suggested: Deal feature)

1. **Create the DTOs**
   - [ ] `Features/Deal/DTOs/DealDto.cs`
   - [ ] `Features/Deal/DTOs/CreateDealDto.cs`

2. **Create the Command**
   - [ ] `Features/Deal/Commands/CreateDeal/CreateDealCommand.cs`
   - [ ] `Features/Deal/Commands/CreateDeal/CreateDealCommandHandler.cs`
   - [ ] `Features/Deal/Commands/CreateDeal/CreateDealCommandValidator.cs`

3. **Create the Query**
   - [ ] `Features/Deal/Queries/GetDeal/GetDealQuery.cs`
   - [ ] `Features/Deal/Queries/GetDeal/GetDealQueryHandler.cs`

4. **Create the Mapping**
   - [ ] Update `Features/Deal/MappingProfile.cs`

5. **Create the Controller**
   - [ ] `Controllers/DealsController.cs`

6. **Test Your Work**
   - [ ] Build and run the application
   - [ ] Test via Swagger UI
   - [ ] Verify data in database

---

## 🎯 Key Concepts to Master

### Must Understand
- [ ] Dependency Injection in .NET
- [ ] Async/await programming
- [ ] Entity Framework Core basics
- [ ] LINQ queries
- [ ] MediatR request/handler pattern
- [ ] AutoMapper profiles

### Nice to Know
- [ ] FluentValidation
- [ ] Clean Architecture principles
- [ ] SOLID principles
- [ ] REST API design

---

## 🆘 Getting Help

### Common Issues

**Build fails with "file locked" error:**
- Close any running instances of the application
- Check Task Manager for `dotnet` or `Replate.Api` processes

**Database connection issues:**
- Verify LocalDB is installed and running
- Check connection string in `appsettings.Development.json`

**Seed data not appearing:**
- Delete all data from tables and restart the app
- Check console output for seeding messages

### Resources
- [MediatR Documentation](https://github.com/jbogard/MediatR/wiki)
- [AutoMapper Documentation](https://docs.automapper.org/)
- [FluentValidation Documentation](https://docs.fluentvalidation.net/)
- [Entity Framework Core Documentation](https://docs.microsoft.com/en-us/ef/core/)

### Team Contacts
- Ask questions in the team chat
- Request code review for your first PR
- Pair programming sessions available

---

## ✅ Completion Checklist

Before considering yourself onboarded:
- [ ] Can build and run the application locally
- [ ] Understand the project architecture
- [ ] Can explain the flow from Controller → MediatR → Handler → Database
- [ ] Have tested all existing API endpoints
- [ ] Have completed your first feature or bug fix
- [ ] Have submitted your first pull request

---

**Welcome to the team! 🎉**
