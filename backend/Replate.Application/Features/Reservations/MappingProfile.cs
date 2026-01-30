using AutoMapper;
using Replate.Application.Features.Reservations.DTOs;
using Replate.Domain.Entities;

namespace Replate.Application.Features.Reservations;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // CreateReservationDto to Reservation
        CreateMap<CreateReservationDto, Reservation>();
        // Reservation to ReservationDto
        CreateMap<Reservation, ReservationDto>();
        // UpdateReservationDto to Reservation
        CreateMap<UpdateReservationDto, Reservation>()
            .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
    }
}