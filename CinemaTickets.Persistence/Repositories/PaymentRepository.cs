using AutoMapper;
using CinemaTickets.Persistence.Entities;
using CinemaTickets.Core.Models;
using CinemaTickets.Persistence.Repositories;
using CinemaTickets.Application.Interfaces.Repositories;
using CinemaTickets.Application.Interfaces.Services;
using Microsoft.EntityFrameworkCore;
using CinemaTickets.Application.Services;

namespace CinemaTickets.Persistence.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly CinemaDbContext _context;
        private IMapper _mapper;
        private ISeatService _seatService;
        public PaymentRepository(CinemaDbContext context, IMapper mapper, ISeatService seatService)
        {
            _context = context;
            _mapper = mapper;
            _seatService = seatService;
        }
        public async Task<Payment> ProcessPayment(User user, decimal amountPaid, string paymentType, int seatId, 
                int seanceId)
        {
            var userEntity = new UserEntity
            {
                Id = user.Id,
                Name = user.Name,
                Surname = user.Surname,
                Email = user.Email,
            };

            var totalCost = _seatService.GetSeatPrice(seatId).Result;

            if (totalCost > amountPaid)
                throw new InvalidOperationException("Недостаточно средств");

            decimal changeGiven = amountPaid - totalCost;

            var paymentEntity = new PaymentEntity
            {
                Id = Guid.NewGuid(),
                UserId = user.Id,
                SeatId = seatId,
                Amount = amountPaid,
                PaymentType = paymentType,
                PaymentTime = DateTime.UtcNow,
                ChangeGiven = changeGiven
            };

            var ticketEntity = await GetInfoForTicket(user, seatId, seanceId, paymentEntity.Id);         

            await _context.Users.AddAsync(userEntity);
            await _context.Payments.AddAsync(paymentEntity);
            await _context.Tickets.AddAsync(ticketEntity);
            await _context.SaveChangesAsync();

            return _mapper.Map<Payment>(paymentEntity);
        }

        public async Task SetSeatAvailabilities(int seatId, int seanceId, SeatAvailabilityEntity seatAvailability)
        {
            var seat = await _context.Seats
                .FirstOrDefaultAsync(s => s.Id == seatId);

            var seance = await _context.Seances
                .FirstOrDefaultAsync(s => s.Id == seanceId);

            seat?.SeatAvailabilities.Add(seatAvailability);
            seance?.SeatAvailabilities.Add(seatAvailability);
        }
        public async Task<TicketEntity> GetInfoForTicket(User user, int seatId, int seanceId, Guid paymentId)
        {
            var seat = await _context.Seats
                .FirstOrDefaultAsync(s => s.Id == seatId);

            var seance = await _context.Seances
                .FirstOrDefaultAsync(s => s.Id == seanceId);

            var hall = await _context.Halls
                .FirstOrDefaultAsync(h => h.Id == seance.HallId);

            var ticketEntity = new TicketEntity
            {
                Id = Guid.NewGuid(),
                SeatId = seatId,
                HallName = hall.Name,
                FilmName = seance.FilmName,
                FilmStartTime = seance.StartTime,
                RowNumber = seat.RowNumber,
                SeatNumber = seat.SeatNumber,
                UserName = user.Name,
                UserSurname = user.Surname,
                IssueTime = DateTime.UtcNow,
                Price = seat.Price,
                PaymentId = paymentId
            };

            return ticketEntity;
        }

        public async Task<Ticket> GetTicket(Guid paymentId)
        {
            var ticket = await _context.Tickets
                .Include(t => t.Seat)
                .Include(t => t.Payment)
                .FirstOrDefaultAsync(t => t.PaymentId == paymentId);

            return _mapper.Map<Ticket>(ticket);
        }
    }
}
