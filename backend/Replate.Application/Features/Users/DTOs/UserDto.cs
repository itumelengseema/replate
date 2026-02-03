﻿using Replate.Domain.Entities;
using Replate.Domain.Enums;

namespace Replate.Application.Features.Users.DTOs;

public class UserDto
{
   
    public Guid PublicId { get; set; } = Guid.NewGuid();
        
    // User Information
    public required string FirebaseUid { get; set; }
    public required string Email { get; set; }
    public string? DisplayName { get; set; }
    public string? PhotoUrl { get; set; }
        
    // User Role
    public UserRole Role { get; set; } = UserRole.Customer;
        
    // Relationships
    public VendorProfile? VendorProfile { get; set; }
    public ICollection<Order>  Orders { get; set; } = new List<Order>();
        
    // Audit fields
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    public bool IsActive { get; set; } = true;
}