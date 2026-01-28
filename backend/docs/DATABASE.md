# Database Schema Documentation

## 📊 Entity Relationship Diagram

```
┌─────────────────┐       ┌─────────────────────┐       ┌─────────────────┐
│      User       │       │    VendorProfile    │       │  VendorAddress  │
├─────────────────┤       ├─────────────────────┤       ├─────────────────┤
│ Id (PK)         │──1:1──│ Id (PK)             │──1:1──│ Id (PK)         │
│ PublicId        │       │ PublicId            │       │ PublicId        │
│ FirebaseUid     │       │ UserId (FK)         │       │ VendorProfileId │
│ Email           │       │ VendorAddressId(FK) │       │ Street          │
│ DisplayName     │       │ BusinessName        │       │ Building        │
│ PhotoUrl        │       │ Description         │       │ City            │
│ Role            │       │ LogoUrl             │       │ Province        │
│ IsActive        │       │ PhoneNumber         │       │ PostalCode      │
│ CreatedAt       │       │ Email               │       │ Country         │
│ UpdatedAt       │       │ BusinessHours       │       │ Latitude        │
└─────────────────┘       │ IsActive            │       │ Longitude       │
        │                 │ CreatedAt           │       │ GooglePlaceId   │
        │                 │ UpdatedAt           │       │ CreatedAt       │
        │                 └─────────────────────┘       │ UpdatedAt       │
        │                          │                    └─────────────────┘
        │                          │
        │                          │ 1:Many
        │                          ▼
        │                 ┌─────────────────┐
        │                 │      Deal       │
        │                 ├─────────────────┤
        │                 │ Id (PK)         │
        │                 │ PublicId        │
        │                 │ VendorProfileId │
        │                 │ Title           │
        │                 │ Description     │
        │                 │ OriginalPrice   │
        │                 │ DiscountedPrice │
        │                 │ AvailableQty    │
        │                 │ DealType        │
        │                 │ Category        │
        │                 │ AvailableFrom   │
        │                 │ AvailableUntil  │
        │                 │ IsActive        │
        │                 │ CreatedAt       │
        │                 │ UpdatedAt       │
        │                 └─────────────────┘
        │                    │           │
        │                    │           │ 1:Many
        │                    │           ▼
        │                    │  ┌─────────────────┐
        │                    │  │    DealItem     │
        │                    │  ├─────────────────┤
        │                    │  │ Id (PK)         │
        │                    │  │ DealId (FK)     │
        │                    │  │ Name            │
        │                    │  │ Quantity        │
        │                    │  └─────────────────┘
        │                    │
        │                    │ 1:Many
        │                    ▼
        │           ┌─────────────────┐
        │           │   Reservation   │
        │  1:Many   ├─────────────────┤
        └──────────▶│ Id (PK)         │
                    │ PublicId        │
                    │ DealId (FK)     │
                    │ UserId (FK)     │
                    │ Quantity        │
                    │ TotalPrice      │
                    │ Status          │
                    │ PickupTime      │
                    │ PickupInstr.    │
                    │ CreatedAt       │
                    │ UpdatedAt       │
                    └─────────────────┘
```

---

## 📋 Entities

### User
Represents a user of the platform (Customer, Vendor, or Admin).

| Column | Type | Constraints | Description |
|--------|------|-------------|-------------|
| `Id` | `int` | PK, Identity | Internal ID |
| `PublicId` | `Guid` | Unique, Required | Public-facing ID (used in APIs) |
| `FirebaseUid` | `string(128)` | Unique, Required | Firebase authentication UID |
| `Email` | `string(256)` | Unique, Required | User email address |
| `DisplayName` | `string(100)` | Nullable | User display name |
| `PhotoUrl` | `string(500)` | Nullable | Profile photo URL |
| `Role` | `int` | Required | User role (enum) |
| `IsActive` | `bool` | Default: true | Soft delete flag |
| `CreatedAt` | `DateTime` | Required | Record creation timestamp |
| `UpdatedAt` | `DateTime` | Nullable | Last update timestamp |

**Relationships:**
- One-to-One with `VendorProfile` (optional, only for Vendor role)
- One-to-Many with `Reservation`

---

### VendorProfile
Business profile for vendors selling food deals.

| Column | Type | Constraints | Description |
|--------|------|-------------|-------------|
| `Id` | `int` | PK, Identity | Internal ID |
| `PublicId` | `Guid` | Unique, Required | Public-facing ID |
| `UserId` | `int` | FK, Required | Associated user |
| `VendorAddressId` | `int` | FK, Nullable | Business address |
| `BusinessName` | `string(200)` | Required | Business name |
| `Description` | `string(1000)` | Nullable | Business description |
| `LogoUrl` | `string(500)` | Nullable | Logo image URL |
| `PhoneNumber` | `string(20)` | Required | Contact phone |
| `Email` | `string(256)` | Required | Business email |
| `BusinessHours` | `string(500)` | Nullable | Operating hours (JSON) |
| `IsActive` | `bool` | Default: true | Active status |
| `CreatedAt` | `DateTime` | Required | Creation timestamp |
| `UpdatedAt` | `DateTime` | Nullable | Update timestamp |

**Relationships:**
- One-to-One with `User`
- One-to-One with `VendorAddress`
- One-to-Many with `Deal`

---

### VendorAddress
Physical address for a vendor's business.

| Column | Type | Constraints | Description |
|--------|------|-------------|-------------|
| `Id` | `int` | PK, Identity | Internal ID |
| `PublicId` | `Guid` | Unique, Required | Public-facing ID |
| `VendorProfileId` | `int` | FK, Required | Associated vendor profile |
| `Street` | `string(200)` | Required | Street address |
| `Building` | `string(150)` | Nullable | Building/suite number |
| `City` | `string(128)` | Required | City name |
| `Province` | `string(128)` | Required | Province/State |
| `PostalCode` | `string(10)` | Required | Postal code |
| `Country` | `string(128)` | Default: "South Africa" | Country |
| `Latitude` | `double` | Required | GPS latitude |
| `Longitude` | `double` | Required | GPS longitude |
| `GooglePlaceId` | `string(200)` | Nullable | Google Places API ID |
| `CreatedAt` | `DateTime` | Required | Creation timestamp |
| `UpdatedAt` | `DateTime` | Nullable | Update timestamp |

---

### Deal
A food deal offered by a vendor.

| Column | Type | Constraints | Description |
|--------|------|-------------|-------------|
| `Id` | `int` | PK, Identity | Internal ID |
| `PublicId` | `Guid` | Unique, Required | Public-facing ID |
| `VendorProfileId` | `int` | FK, Required | Vendor offering the deal |
| `Title` | `string(200)` | Required | Deal title |
| `Description` | `string(1000)` | Nullable | Deal description |
| `OriginalPrice` | `decimal(18,2)` | Required | Original price |
| `DiscountedPrice` | `decimal(18,2)` | Required | Sale price |
| `AvailableQuantity` | `int` | Required | Available units |
| `DealType` | `int` | Required | Type (enum) |
| `Category` | `int` | Required | Food category (enum) |
| `AvailableFrom` | `DateTime` | Required | Start availability |
| `AvailableUntil` | `DateTime` | Required | End availability |
| `IsActive` | `bool` | Default: true | Active status |
| `CreatedAt` | `DateTime` | Required | Creation timestamp |
| `UpdatedAt` | `DateTime` | Nullable | Update timestamp |

**Relationships:**
- Many-to-One with `VendorProfile`
- One-to-Many with `DealItem`
- One-to-Many with `Reservation`

---

### Reservation
A customer's reservation for a deal.

| Column | Type | Constraints | Description |
|--------|------|-------------|-------------|
| `Id` | `int` | PK, Identity | Internal ID |
| `PublicId` | `Guid` | Unique, Required | Public-facing ID |
| `DealId` | `int` | FK, Required | Reserved deal |
| `UserId` | `int` | FK, Required | Customer who reserved |
| `Quantity` | `int` | Required | Number of units |
| `TotalPrice` | `decimal(18,2)` | Required | Total amount |
| `Status` | `int` | Required | Reservation status (enum) |
| `PickupTime` | `DateTime` | Required | Scheduled pickup time |
| `PickupInstructions` | `string(500)` | Nullable | Special instructions |
| `CreatedAt` | `DateTime` | Required | Creation timestamp |
| `UpdatedAt` | `DateTime` | Required | Update timestamp |

---

## 📊 Enumerations

### UserRole
```csharp
public enum UserRole
{
    Customer = 0,   // Regular customer
    Vendor = 1,     // Food vendor/seller
    Admin = 2       // Platform administrator
}
```

### DealType
```csharp
public enum DealType
{
    SurpriseBag = 0,    // Mystery bag with random items
    SpecificItems = 1   // Known specific items
}
```

### FoodCategory
```csharp
public enum FoodCategory
{
    Unknown = 0,
    Bakery = 1,      // Bread, pastries, cakes
    FastFood = 2,    // Burgers, pizza, etc.
    Grocery = 3,     // Groceries, produce
    Beverages = 4,   // Drinks, coffee
    Other = 5        // Other food types
}
```

### ReservationStatus
```csharp
public enum ReservationStatus
{
    Pending = 0,     // Awaiting confirmation
    Confirmed = 1,   // Confirmed by vendor
    PickedUp = 2,    // Customer picked up
    Cancelled = 3,   // Cancelled
    NoShow = 4       // Customer didn't show up
}
```

---

## 🔧 Database Migrations

### Creating a New Migration
```bash
cd Replate.Api
dotnet ef migrations add <MigrationName> --project ../Replate.Infrastructure
```

### Applying Migrations
```bash
dotnet ef database update --project ../Replate.Infrastructure
```

### Reverting a Migration
```bash
dotnet ef database update <PreviousMigrationName> --project ../Replate.Infrastructure
```

---

## 🌱 Seed Data

The application automatically seeds test data on startup if the database is empty:

- **4 Users**: 2 Vendors, 2 Customers
- **2 Vendor Profiles**: Joe's Pizza, Healthy Bites Café
- **2 Vendor Addresses**: Johannesburg, Pretoria locations

See `Replate.Infrastructure/Persistence/Seeds/ApplicationDbContextSeed.cs` for details.
