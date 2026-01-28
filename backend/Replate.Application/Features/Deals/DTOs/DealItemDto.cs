using Replate.Domain.Entities;
using Replate.Domain.Enums;

namespace Replate.Application.Features.Deals.DTOs;

public class DealItemDto
{
 
   public int Id { get; set; }
   public string Name { get; set; } = string.Empty;
   public string Description { get; set; } = string.Empty;
   public int Quantity { get; set; }
   public decimal UnitPrice { get; set; }


}