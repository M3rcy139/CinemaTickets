using CinemaTickets.Persistence.Entities;
using CinemaTickets.Core.Models;
using AutoMapper;

namespace CinemaTickets.Persistence.Mappings
{
    public class DataBaseMappings : Profile
    {
        public DataBaseMappings() 
        { 
            CreateMap<HallEntity, Hall>();
            CreateMap<PaymentEntity, Payment>();
            CreateMap<SeanceEntity, Seance>();
            CreateMap<SeatEntity, Seat>();
            CreateMap<TicketEntity, Ticket>();
            CreateMap<UserEntity, User>();
        }
        
    }
}
