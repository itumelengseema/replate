# Replate Backend API

## ?? Overview

**Replate** is a food deals platform that connects vendors (restaurants, bakeries, grocery stores) with customers looking for discounted food items. The platform helps reduce food waste by allowing vendors to sell surplus food at discounted prices.

---

## ?? Quick Start

### Prerequisites
- .NET 10 SDK
- SQL Server (LocalDB or full instance)
- Visual Studio 2022 / Rider / VS Code

### Running the API

```bash
cd backend
dotnet restore
dotnet run --project Replate.Api
```

The API will start at:
- **HTTP**: `http://localhost:5041`
- **HTTPS**: `https://localhost:7102`
- **Swagger**: `http://localhost:5041/swagger`

---

## ??? Architecture

This project follows **Clean Architecture** principles with **CQRS pattern** using MediatR.

```
???????????????????????????????????????????????????????????????
?                      Replate.Api                            ?
?                   (Presentation Layer)                      ?
???????????????????????????????????????????????????????????????
?                  Replate.Application                        ?
?                   (Application Layer)                       ?
???????????????????????????????????????????????????????????????
?                  Replate.Infrastructure                     ?
?                 (Infrastructure Layer)                      ?
???????????????????????????????????????????????????????????????
?                    Replate.Domain                           ?
?                    (Domain Layer)                           ?
???????????????????????????????????????????????????????????????
```

---

## ?? API Endpoints

### Base URL
```
http://localhost:5041/api
```

### FoodListings

| Method | Endpoint | Description |
|--------|----------|-------------|
| `GET` | `/FoodListings` | Get all active food listings |
| `GET` | `/FoodListings/{publicId}` | Get food listing by ID |
| `POST` | `/FoodListings?vendorProfileId={id}` | Create a new food listing |
| `PUT` | `/FoodListings/{publicId}` | Update a food listing |
| `DELETE` | `/FoodListings/{publicId}` | Delete a food listing |

**Create FoodListing Request Body:**
```json
{
  "title": "Fresh Bakery Bundle",
  "description": "Assorted pastries and bread",
  "originalPrice": 25.00,
  "discountedPrice": 12.50,
  "quantityAvailable": 10,
  "category": 1,
  "listingType": 1,
  "availableFrom": "2024-01-15T08:00:00Z",
  "availableUntil": "2024-01-15T18:00:00Z",
  "imageUrl": "https://example.com/image.jpg",
  "items": [
    {
      "name": "Croissant",
      "quantity": 3,
      "description": "Butter croissant"
    }
  ]
}
```

**FoodListing Response:**
```json
{
  "publicId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "title": "Fresh Bakery Bundle",
  "description": "Assorted pastries and bread",
  "originalPrice": 25.00,
  "discountedPrice": 12.50,
  "quantityAvailable": 10,
  "category": 1,
  "listingType": 1,
  "availableFrom": "2024-01-15T08:00:00Z",
  "availableUntil": "2024-01-15T18:00:00Z",
  "imageUrl": "https://example.com/image.jpg",
  "vendorPublicId": "...",
  "vendorName": "Joe's Bakery",
  "isActive": true,
  "createdAt": "2024-01-15T07:00:00Z",
  "items": [...]
}
```

---

### FoodListingItems

| Method | Endpoint | Description |
|--------|----------|-------------|
| `GET` | `/FoodListingItems/{id}` | Get item by ID |
| `GET` | `/FoodListingItems/by-food-listing/{foodListingPublicId}` | Get all items for a listing |
| `POST` | `/FoodListingItems` | Create a new item |
| `PUT` | `/FoodListingItems/{id}` | Update an item |
| `DELETE` | `/FoodListingItems/{id}` | Delete an item |

**Create FoodListingItem Request:**
```json
{
  "name": "Chocolate Muffin",
  "quantity": 5,
  "description": "Fresh baked chocolate muffin",
  "foodListingPublicId": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
}
```

---

### Orders

| Method | Endpoint | Description |
|--------|----------|-------------|
| `GET` | `/Orders` | Get all orders |
| `GET` | `/Orders/{publicId}` | Get order by ID |
| `GET` | `/Orders/user/{userId}` | Get orders by user |
| `POST` | `/Orders?userId={id}` | Create a new order |
| `PUT` | `/Orders/{publicId}` | Update an order |
| `POST` | `/Orders/{publicId}/cancel` | Cancel an order |

**Create Order Request:**
```json
{
  "foodListingPublicId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "quantity": 2,
  "notes": "Please include napkins",
  "pickupTime": "2024-01-15T12:00:00Z"
}
```

**Order Response:**
```json
{
  "publicId": "...",
  "quantity": 2,
  "totalPrice": 25.00,
  "status": 0,
  "notes": "Please include napkins",
  "pickupTime": "2024-01-15T12:00:00Z",
  "userPublicId": "...",
  "userDisplayName": "John Doe",
  "foodListingPublicId": "...",
  "foodListingTitle": "Fresh Bakery Bundle",
  "createdAt": "2024-01-15T10:00:00Z"
}
```

**Order Status Values:**
| Value | Status |
|-------|--------|
| 0 | Pending |
| 1 | Confirmed |
| 2 | InProgress |
| 3 | Ready |
| 4 | Completed |
| 5 | Cancelled |

---

### VendorProfiles

| Method | Endpoint | Description |
|--------|----------|-------------|
| `GET` | `/VendorProfiles/{publicId}` | Get vendor profile by ID |
| `GET` | `/VendorProfiles/user/{userId}` | Get vendor profile by user ID |
| `POST` | `/VendorProfiles?userId={id}` | Create a vendor profile |
| `PUT` | `/VendorProfiles/{publicId}` | Update a vendor profile |

---

## ?? Enums

### FoodCategory
| Value | Name |
|-------|------|
| 0 | Unknown |
| 1 | Bakery |
| 2 | FastFood |
| 3 | Grocery |
| 4 | Beverages |
| 5 | Other |

### FoodListingType
| Value | Name |
|-------|------|
| 0 | Unknown |
| 1 | Discount |
| 2 | FreeSample |
| 3 | Bundle |
| 4 | Clearance |

### OrderStatus
| Value | Name |
|-------|------|
| 0 | Pending |
| 1 | Confirmed |
| 2 | InProgress |
| 3 | Ready |
| 4 | Completed |
| 5 | Cancelled |

### UserRole
| Value | Name |
|-------|------|
| 0 | Customer |
| 1 | Vendor |
| 2 | Admin |

---

## ?? Configuration

### appsettings.json
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=ReplateDb;Trusted_Connection=True;"
  }
}
```

---

## ?? Mobile App Integration

### Base URLs by Platform

| Platform | URL |
|----------|-----|
| Android Emulator | `http://10.0.2.2:5041/api` |
| iOS Simulator | `http://localhost:5041/api` |
| Physical Device | `http://<YOUR_IP>:5041/api` |

### Flutter Example

```dart
import 'dart:convert';
import 'package:http/http.dart' as http;

class ApiService {
  // Use 10.0.2.2 for Android emulator, localhost for iOS
  static const String baseUrl = 'http://10.0.2.2:5041/api';

  // Get all food listings
  Future<List<dynamic>> getFoodListings() async {
    final response = await http.get(
      Uri.parse('$baseUrl/FoodListings'),
      headers: {'Accept': 'application/json'},
    );
    
    if (response.statusCode == 200) {
      return jsonDecode(response.body);
    }
    throw Exception('Failed to load food listings');
  }

  // Get food listing by ID
  Future<Map<String, dynamic>> getFoodListing(String publicId) async {
    final response = await http.get(
      Uri.parse('$baseUrl/FoodListings/$publicId'),
      headers: {'Accept': 'application/json'},
    );
    
    if (response.statusCode == 200) {
      return jsonDecode(response.body);
    }
    throw Exception('Food listing not found');
  }

  // Create an order
  Future<Map<String, dynamic>> createOrder({
    required int userId,
    required String foodListingPublicId,
    required int quantity,
    String? notes,
    DateTime? pickupTime,
  }) async {
    final response = await http.post(
      Uri.parse('$baseUrl/Orders?userId=$userId'),
      headers: {
        'Content-Type': 'application/json',
        'Accept': 'application/json',
      },
      body: jsonEncode({
        'foodListingPublicId': foodListingPublicId,
        'quantity': quantity,
        'notes': notes,
        'pickupTime': pickupTime?.toIso8601String(),
      }),
    );
    
    if (response.statusCode == 201) {
      return jsonDecode(response.body);
    }
    throw Exception('Failed to create order: ${response.body}');
  }

  // Get user's orders
  Future<List<dynamic>> getUserOrders(int userId) async {
    final response = await http.get(
      Uri.parse('$baseUrl/Orders/user/$userId'),
      headers: {'Accept': 'application/json'},
    );
    
    if (response.statusCode == 200) {
      return jsonDecode(response.body);
    }
    throw Exception('Failed to load orders');
  }

  // Cancel an order
  Future<void> cancelOrder(String publicId) async {
    final response = await http.post(
      Uri.parse('$baseUrl/Orders/$publicId/cancel'),
      headers: {'Accept': 'application/json'},
    );
    
    if (response.statusCode != 204) {
      throw Exception('Failed to cancel order');
    }
  }
}
```

### Flutter Models

```dart
// food_listing.dart
class FoodListing {
  final String publicId;
  final String title;
  final String? description;
  final double originalPrice;
  final double discountedPrice;
  final int quantityAvailable;
  final int category;
  final int listingType;
  final DateTime availableFrom;
  final DateTime availableUntil;
  final String? imageUrl;
  final String vendorPublicId;
  final String vendorName;
  final bool isActive;

  FoodListing({
    required this.publicId,
    required this.title,
    this.description,
    required this.originalPrice,
    required this.discountedPrice,
    required this.quantityAvailable,
    required this.category,
    required this.listingType,
    required this.availableFrom,
    required this.availableUntil,
    this.imageUrl,
    required this.vendorPublicId,
    required this.vendorName,
    required this.isActive,
  });

  factory FoodListing.fromJson(Map<String, dynamic> json) {
    return FoodListing(
      publicId: json['publicId'],
      title: json['title'],
      description: json['description'],
      originalPrice: json['originalPrice'].toDouble(),
      discountedPrice: json['discountedPrice'].toDouble(),
      quantityAvailable: json['quantityAvailable'],
      category: json['category'],
      listingType: json['listingType'],
      availableFrom: DateTime.parse(json['availableFrom']),
      availableUntil: DateTime.parse(json['availableUntil']),
      imageUrl: json['imageUrl'],
      vendorPublicId: json['vendorPublicId'],
      vendorName: json['vendorName'],
      isActive: json['isActive'],
    );
  }

  double get discountPercentage => 
    ((originalPrice - discountedPrice) / originalPrice * 100).roundToDouble();
}

// order.dart
enum OrderStatus { pending, confirmed, inProgress, ready, completed, cancelled }

class Order {
  final String publicId;
  final int quantity;
  final double totalPrice;
  final OrderStatus status;
  final String? notes;
  final DateTime? pickupTime;
  final String foodListingPublicId;
  final String foodListingTitle;
  final DateTime createdAt;

  Order({
    required this.publicId,
    required this.quantity,
    required this.totalPrice,
    required this.status,
    this.notes,
    this.pickupTime,
    required this.foodListingPublicId,
    required this.foodListingTitle,
    required this.createdAt,
  });

  factory Order.fromJson(Map<String, dynamic> json) {
    return Order(
      publicId: json['publicId'],
      quantity: json['quantity'],
      totalPrice: json['totalPrice'].toDouble(),
      status: OrderStatus.values[json['status']],
      notes: json['notes'],
      pickupTime: json['pickupTime'] != null 
        ? DateTime.parse(json['pickupTime']) 
        : null,
      foodListingPublicId: json['foodListingPublicId'],
      foodListingTitle: json['foodListingTitle'],
      createdAt: DateTime.parse(json['createdAt']),
    );
  }
}
```

---

## ?? Testing

### Using Swagger
Navigate to `http://localhost:5041/swagger` to test all endpoints interactively.

### Using api.http
An `api.http` file is auto-generated in the project folder when the API starts.

---

## ?? Error Responses

All error responses follow this format:

```json
{
  "error": "Error message here"
}
```

Validation errors:
```json
{
  "errors": {
    "Title": ["Title is required"],
    "OriginalPrice": ["Original price must be non-negative"]
  }
}
```

---

## ??? Tech Stack

- **.NET 10** - Framework
- **Entity Framework Core** - ORM
- **MediatR** - CQRS & Mediator pattern
- **AutoMapper** - Object mapping
- **FluentValidation** - Request validation
- **SQL Server** - Database
- **Swagger/OpenAPI** - API documentation
