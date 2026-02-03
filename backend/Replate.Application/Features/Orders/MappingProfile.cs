﻿using AutoMapper;
using Replate.Application.Features.Orders.DTOs;
using Replate.Application.Features.Reservations.DTOs;
using Replate.Domain.Entities;

namespace Replate.Application.Features.Orders;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // CreateOrderDto to Order
        CreateMap<CreateOrderDto, Order>();
        // Order to OrderDto
        CreateMap<Order, OrderDto>();
        // UpdateOrderDto to Order
        CreateMap<UpdateOrderDto, Order>()
            .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
    }
}