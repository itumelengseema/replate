# API Reference

## 🌐 Base URL

- **Development**: `https://localhost:5001` or `http://localhost:5000`
- **Swagger UI**: Available at root URL (`/`)

---

## 🔐 Authentication

> **Note**: Authentication is currently disabled for development. Firebase authentication will be implemented.

Future authentication will use Firebase JWT tokens:

```http
Authorization: Bearer <firebase-id-token>
```

---

## 📡 Endpoints

### Vendor Profiles

#### Create Vendor Profile

Creates a new vendor profile for an existing user.

```http
POST /api/vendorprofiles?userId={userId}
Content-Type: application/json
```

**Query Parameters:**
| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `userId` | `int` | Yes | The user ID (temporary - will come from JWT) |

**Request Body:**
```json
{
  "businessName": "Joe's Pizza",
  "description": "Best pizza in Jozi! Family-owned since 1985.",
  "logoUrl": "https://example.com/logo.png",
  "phoneNumber": "0118452367",
  "email": "info@joespizza.com",
  "businessHours": "Mon-Fri: 11am-10pm, Sat-Sun: 12pm-11pm",
  "street": "123 Main Street",
  "building": "Suite 100",
  "city": "Johannesburg",
  "province": "Gauteng",
  "postalCode": "1510",
  "country": "South Africa",
  "latitude": -26.2041,
  "longitude": 28.0473
}
```

**Response:** `201 Created`
```json
{
  "publicId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "businessName": "Joe's Pizza",
  "description": "Best pizza in Jozi! Family-owned since 1985.",
  "logoUrl": "https://example.com/logo.png",
  "phoneNumber": "0118452367",
  "email": "info@joespizza.com",
  "businessHours": "Mon-Fri: 11am-10pm, Sat-Sun: 12pm-11pm",
  "isActive": true,
  "createdAt": "2026-01-28T10:30:00Z",
  "address": {
    "publicId": "4fa85f64-5717-4562-b3fc-2c963f66afa7",
    "street": "123 Main Street",
    "building": "Suite 100",
    "city": "Johannesburg",
    "province": "Gauteng",
    "postalCode": "1510",
    "country": "South Africa",
    "latitude": -26.2041,
    "longitude": 28.0473
  }
}
```

**Error Responses:**
- `400 Bad Request`: Validation errors or user already has a profile
- `404 Not Found`: User not found

---

#### Get Vendor Profile by Public ID

Retrieves a vendor profile by its public ID.

```http
GET /api/vendorprofiles/{publicId}
```

**Path Parameters:**
| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `publicId` | `Guid` | Yes | The vendor profile's public ID |

**Response:** `200 OK`
```json
{
  "publicId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "businessName": "Joe's Pizza",
  "description": "Best pizza in Jozi!",
  "phoneNumber": "0118452367",
  "email": "info@joespizza.com",
  "businessHours": "Mon-Fri: 11am-10pm",
  "isActive": true,
  "createdAt": "2026-01-28T10:30:00Z",
  "address": { ... }
}
```

**Error Responses:**
- `404 Not Found`: Vendor profile not found

---

#### Get Vendor Profile by User ID

Retrieves a vendor profile for a specific user.

```http
GET /api/vendorprofiles/user/{userId}
```

**Path Parameters:**
| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `userId` | `int` | Yes | The user ID |

**Response:** `200 OK` (same as above)

**Error Responses:**
- `404 Not Found`: No vendor profile for this user

---

#### Update Vendor Profile

Updates an existing vendor profile. Only non-null fields are updated.

```http
PUT /api/vendorprofiles/{publicId}
Content-Type: application/json
```

**Path Parameters:**
| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `publicId` | `Guid` | Yes | The vendor profile's public ID |

**Request Body:** (all fields optional)
```json
{
  "businessName": "Joe's Amazing Pizza",
  "description": "Updated description",
  "phoneNumber": "0119999999",
  "email": "new@joespizza.com",
  "businessHours": "24/7",
  "street": "456 New Street",
  "city": "Pretoria"
}
```

**Response:** `200 OK`
```json
{
  "publicId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "businessName": "Joe's Amazing Pizza",
  ...
}
```

**Error Responses:**
- `400 Bad Request`: Validation errors
- `404 Not Found`: Vendor profile not found

---

## 📋 Data Types

### VendorProfileDto

```typescript
interface VendorProfileDto {
  publicId: string;          // GUID
  businessName: string;
  description: string | null;
  logoUrl: string | null;
  phoneNumber: string;
  email: string;
  businessHours: string | null;
  isActive: boolean;
  createdAt: string;         // ISO 8601 datetime
  updatedAt: string | null;  // ISO 8601 datetime
  address: VendorAddressDto | null;
}
```

### VendorAddressDto

```typescript
interface VendorAddressDto {
  publicId: string;          // GUID
  street: string;
  building: string | null;
  city: string;
  province: string;
  postalCode: string;
  country: string;
  latitude: number;
  longitude: number;
  googlePlaceId: string | null;
}
```

### CreateVendorProfileDto

```typescript
interface CreateVendorProfileDto {
  businessName: string;      // Required, max 200 chars
  description?: string;      // Optional, max 1000 chars
  logoUrl?: string;          // Optional, max 500 chars
  phoneNumber: string;       // Required, max 20 chars
  email: string;             // Required, valid email
  businessHours?: string;    // Optional
  street: string;            // Required
  building?: string;         // Optional
  city: string;              // Required
  province: string;          // Required
  postalCode: string;        // Required
  country?: string;          // Optional, defaults to "South Africa"
  latitude: number;          // Required
  longitude: number;         // Required
}
```

### UpdateVendorProfileDto

```typescript
interface UpdateVendorProfileDto {
  businessName?: string;
  description?: string;
  logoUrl?: string;
  phoneNumber?: string;
  email?: string;
  businessHours?: string;
  street?: string;
  building?: string;
  city?: string;
  province?: string;
  postalCode?: string;
  country?: string;
  latitude?: number;
  longitude?: number;
}
```

---

## ❌ Error Responses

All error responses follow this format:

```json
{
  "error": "Error message describing what went wrong"
}
```

For validation errors:
```json
{
  "errors": [
    "BusinessName is required",
    "Email must be a valid email address"
  ]
}
```

### HTTP Status Codes

| Code | Meaning | When Used |
|------|---------|-----------|
| `200 OK` | Success | GET, PUT operations |
| `201 Created` | Resource created | POST operations |
| `400 Bad Request` | Invalid input | Validation errors |
| `401 Unauthorized` | Not authenticated | Missing/invalid token |
| `403 Forbidden` | Not authorized | Insufficient permissions |
| `404 Not Found` | Resource not found | Entity doesn't exist |
| `500 Internal Server Error` | Server error | Unexpected exceptions |

---

## 🧪 Testing with Swagger

1. Start the application: `dotnet run --project Replate.Api`
2. Open browser: `https://localhost:5001`
3. Use the Swagger UI to test endpoints

### Test Flow

1. **Check seed data**: Database is seeded with test users on startup
2. **Get user IDs**: Use SQL or check seed data (Users: 1, 2 = Vendors; 3, 4 = Customers)
3. **Create profile**: POST to `/api/vendorprofiles?userId=1` with profile data
4. **Get profile**: Use the returned `publicId` to GET the profile

---

## 🔮 Upcoming Endpoints

### Deals (TODO)
- `POST /api/deals` - Create a new deal
- `GET /api/deals` - List all active deals
- `GET /api/deals/{publicId}` - Get deal by ID
- `PUT /api/deals/{publicId}` - Update a deal
- `DELETE /api/deals/{publicId}` - Deactivate a deal

### Reservations (TODO)
- `POST /api/reservations` - Create a reservation
- `GET /api/reservations` - List user's reservations
- `GET /api/reservations/{publicId}` - Get reservation details
- `PUT /api/reservations/{publicId}/status` - Update reservation status

### Users (TODO)
- `POST /api/users/register` - Register new user
- `GET /api/users/me` - Get current user profile
- `PUT /api/users/me` - Update user profile
