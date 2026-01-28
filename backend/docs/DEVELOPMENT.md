# Development Guidelines

## 🏗️ Architecture Patterns

### Clean Architecture

This project follows **Clean Architecture** with the following dependency rules:

```
Domain ← Application ← Infrastructure
                    ← Api
```

- **Domain**: No dependencies. Contains entities, enums, and core business rules.
- **Application**: Depends only on Domain. Contains business logic, DTOs, and interfaces.
- **Infrastructure**: Depends on Application and Domain. Contains database access, external services.
- **Api**: Depends on Application and Infrastructure. Contains controllers, middleware.

### CQRS with MediatR

We use **Command Query Responsibility Segregation (CQRS)** pattern:

- **Commands**: Operations that modify state (Create, Update, Delete)
- **Queries**: Operations that read state (Get, List)

```csharp
// Command example
public record CreateVendorProfileCommand : IRequest<Result<VendorProfileDto>>
{
    public int UserId { get; set; }
    public CreateVendorProfileDto VendorProfile { get; set; }
}

// Query example
public record GetVendorProfileQuery : IRequest<Result<VendorProfileDto>>
{
    public Guid PublicId { get; set; }
}
```

---

## 📁 Feature Organization

Features are organized by domain concept, not by technical layer:

```
Features/
├── VendorProfiles/
│   ├── Commands/
│   │   ├── CreateVendorProfile/
│   │   │   ├── CreateVendorProfileCommand.cs
│   │   │   ├── CreateVendorProfileCommandHandler.cs
│   │   │   └── CreateVendorProfileCommandValidator.cs
│   │   └── UpdateVendorProfile/
│   │       ├── UpdateVendorProfileCommand.cs
│   │       ├── UpdateVendorProfileCommandHandler.cs
│   │       └── UpdateVendorProfileCommandValidator.cs
│   ├── Queries/
│   │   ├── GetVendorProfile/
│   │   │   ├── GetVendorProfileQuery.cs
│   │   │   └── GetVendorProfileQueryHandler.cs
│   │   └── GetVendorProfileByUserId/
│   │       ├── GetVendorProfileByUserIdQuery.cs
│   │       └── GetVendorProfileByUserIdQueryHandler.cs
│   ├── DTOs/
│   │   ├── VendorProfileDto.cs
│   │   ├── CreateVendorProfileDto.cs
│   │   └── UpdateVendorProfileDto.cs
│   └── MappingProfile.cs
└── Deal/
    └── ... (similar structure)
```

---

## 🔧 Creating a New Feature

### Step 1: Create the Command/Query

```csharp
// Features/YourFeature/Commands/CreateSomething/CreateSomethingCommand.cs
using MediatR;
using Replate.Application.Common.Models;

namespace Replate.Application.Features.YourFeature.Commands.CreateSomething;

public record CreateSomethingCommand : IRequest<Result<SomethingDto>>
{
    public required string Name { get; init; }
    public string? Description { get; init; }
}
```

### Step 2: Create the Handler

```csharp
// Features/YourFeature/Commands/CreateSomething/CreateSomethingCommandHandler.cs
using AutoMapper;
using MediatR;
using Replate.Application.Common.Models;
using Replate.Application.Interface;

namespace Replate.Application.Features.YourFeature.Commands.CreateSomething;

public class CreateSomethingCommandHandler 
    : IRequestHandler<CreateSomethingCommand, Result<SomethingDto>>
{
    private readonly IApplicationDbContext _db;
    private readonly IMapper _mapper;

    // ✅ ALWAYS use constructor injection
    public CreateSomethingCommandHandler(IApplicationDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<Result<SomethingDto>> Handle(
        CreateSomethingCommand request, 
        CancellationToken cancellationToken)
    {
        // 1. Validate business rules
        // 2. Create entity
        // 3. Save to database
        // 4. Map to DTO and return

        var entity = _mapper.Map<Something>(request);
        
        _db.Somethings.Add(entity);
        await _db.SaveChangesAsync(cancellationToken);

        var dto = _mapper.Map<SomethingDto>(entity);
        return Result<SomethingDto>.Success(dto);
    }
}
```

### Step 3: Create the Validator (Optional but Recommended)

```csharp
// Features/YourFeature/Commands/CreateSomething/CreateSomethingCommandValidator.cs
using FluentValidation;

namespace Replate.Application.Features.YourFeature.Commands.CreateSomething;

public class CreateSomethingCommandValidator : AbstractValidator<CreateSomethingCommand>
{
    public CreateSomethingCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required")
            .MaximumLength(200).WithMessage("Name cannot exceed 200 characters");

        RuleFor(x => x.Description)
            .MaximumLength(1000).WithMessage("Description cannot exceed 1000 characters");
    }
}
```

### Step 4: Create DTOs

```csharp
// Features/YourFeature/DTOs/SomethingDto.cs
namespace Replate.Application.Features.YourFeature.DTOs;

public class SomethingDto
{
    public Guid PublicId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; }
}

// Features/YourFeature/DTOs/CreateSomethingDto.cs
public class CreateSomethingDto
{
    public required string Name { get; set; }
    public string? Description { get; set; }
}
```

### Step 5: Create Mapping Profile

```csharp
// Features/YourFeature/MappingProfile.cs
using AutoMapper;
using Replate.Application.Features.YourFeature.DTOs;
using Replate.Domain.Entities;

namespace Replate.Application.Features.YourFeature;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Entity to DTO
        CreateMap<Something, SomethingDto>();
        
        // DTO to Entity
        CreateMap<CreateSomethingDto, Something>();
    }
}
```

### Step 6: Create Controller Endpoint

```csharp
// Controllers/SomethingsController.cs
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Replate.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class SomethingsController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(SomethingDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateSomethingDto dto)
    {
        var command = new CreateSomethingCommand
        {
            Name = dto.Name,
            Description = dto.Description
        };

        var result = await mediator.Send(command);

        if (!result.IsSuccess)
        {
            return BadRequest(new { error = result.ErrorMessage });
        }

        return CreatedAtAction(
            nameof(GetById),
            new { publicId = result.Data!.PublicId },
            result.Data);
    }
}
```

---

## 📏 Coding Standards

### Naming Conventions

| Type | Convention | Example |
|------|------------|---------|
| Classes | PascalCase | `VendorProfileService` |
| Interfaces | IPascalCase | `IApplicationDbContext` |
| Methods | PascalCase | `GetVendorProfile()` |
| Properties | PascalCase | `BusinessName` |
| Private fields | _camelCase | `_dbContext` |
| Parameters | camelCase | `vendorProfile` |
| Constants | PascalCase | `MaxRetries` |

### File Naming

| Type | Convention | Example |
|------|------------|---------|
| Commands | `{Action}{Entity}Command.cs` | `CreateVendorProfileCommand.cs` |
| Handlers | `{Action}{Entity}CommandHandler.cs` | `CreateVendorProfileCommandHandler.cs` |
| Queries | `{Action}{Entity}Query.cs` | `GetVendorProfileQuery.cs` |
| DTOs | `{Entity}Dto.cs` | `VendorProfileDto.cs` |
| Validators | `{Command}Validator.cs` | `CreateVendorProfileCommandValidator.cs` |

### Code Style

```csharp
// ✅ DO: Use primary constructors for simple classes
public class VendorProfilesController(IMediator mediator) : ControllerBase
{
}

// ✅ DO: Use constructor injection for handlers
public class CreateVendorProfileCommandHandler
{
    private readonly IApplicationDbContext _db;
    private readonly IMapper _mapper;

    public CreateVendorProfileCommandHandler(IApplicationDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }
}

// ❌ DON'T: Use parameterless constructors with self-assignment
public class BadHandler
{
    private readonly IApplicationDbContext _db;
    
    public BadHandler()
    {
        _db = _db; // This is WRONG - always null!
    }
}

// ✅ DO: Use Result<T> for operation results
return Result<VendorProfileDto>.Success(dto);
return Result<VendorProfileDto>.Failure("Error message");

// ✅ DO: Use async/await properly
public async Task<Result<T>> Handle(...)
{
    var entity = await _db.Entities.FirstOrDefaultAsync(...);
    await _db.SaveChangesAsync(cancellationToken);
}
```

---

## 🔒 ID Strategy

We use a **dual ID strategy**:

1. **Internal ID (`Id`)**: Auto-increment integer, used for database relationships
2. **Public ID (`PublicId`)**: GUID, exposed in APIs

```csharp
// Entity
public class VendorProfile
{
    public int Id { get; set; }                    // Internal, never exposed
    public Guid PublicId { get; set; }             // Public, used in APIs
}

// API endpoint uses PublicId
[HttpGet("{publicId:guid}")]
public async Task<IActionResult> GetVendorProfile(Guid publicId)
```

**Benefits:**
- Internal IDs are efficient for joins and indexes
- Public IDs don't reveal record counts or sequence
- Safer for external APIs

---

## 🗃️ Database Conventions

### Entity Configuration

Place EF configurations in `Infrastructure/Persistence/Configurations/`:

```csharp
public class VendorProfileConfiguration : IEntityTypeConfiguration<VendorProfile>
{
    public void Configure(EntityTypeBuilder<VendorProfile> entity)
    {
        entity.ToTable("VendorProfiles");
        entity.HasKey(e => e.Id);
        
        entity.HasIndex(e => e.PublicId).IsUnique();
        
        entity.Property(e => e.BusinessName)
            .IsRequired()
            .HasMaxLength(200);
    }
}
```

### Audit Fields

All entities should have audit fields:

```csharp
public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
public DateTime? UpdatedAt { get; set; }
```

These are automatically set by `ReplateDbContext.SaveChangesAsync()`.

---

## 🧪 Testing Guidelines

### Unit Tests

Test handlers in isolation:

```csharp
[Fact]
public async Task Handle_ValidRequest_ReturnsSuccess()
{
    // Arrange
    var dbContext = CreateInMemoryDbContext();
    var mapper = CreateMapper();
    var handler = new CreateVendorProfileCommandHandler(dbContext, mapper);
    
    var command = new CreateVendorProfileCommand { ... };

    // Act
    var result = await handler.Handle(command, CancellationToken.None);

    // Assert
    Assert.True(result.IsSuccess);
    Assert.NotNull(result.Data);
}
```

### Integration Tests

Test full request pipeline:

```csharp
[Fact]
public async Task CreateVendorProfile_ReturnsCreated()
{
    // Arrange
    var client = _factory.CreateClient();
    var dto = new CreateVendorProfileDto { ... };

    // Act
    var response = await client.PostAsJsonAsync("/api/vendorprofiles?userId=1", dto);

    // Assert
    Assert.Equal(HttpStatusCode.Created, response.StatusCode);
}
```

---

## 🐛 Common Mistakes to Avoid

1. **Wrong constructor injection** (most common!)
   ```csharp
   // ❌ WRONG
   public MyHandler()
   {
       _db = _db;  // Always null!
   }
   
   // ✅ CORRECT
   public MyHandler(IApplicationDbContext db)
   {
       _db = db;
   }
   ```

2. **Forgetting CancellationToken**
   ```csharp
   // ❌ WRONG
   await _db.SaveChangesAsync();
   
   // ✅ CORRECT
   await _db.SaveChangesAsync(cancellationToken);
   ```

3. **Exposing internal IDs**
   ```csharp
   // ❌ WRONG
   [HttpGet("{id:int}")]
   
   // ✅ CORRECT
   [HttpGet("{publicId:guid}")]
   ```

4. **Missing null checks**
   ```csharp
   // ❌ WRONG
   var profile = await _db.VendorProfiles.FirstOrDefaultAsync(...);
   return _mapper.Map<VendorProfileDto>(profile); // May throw!
   
   // ✅ CORRECT
   if (profile == null)
       return Result<VendorProfileDto>.Failure("Not found");
   ```
