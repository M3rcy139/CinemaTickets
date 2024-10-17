using AutoMapper;
using CinemaTickets.Core.Models;
using CinemaTickets.Persistence.Entities;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using CinemaTickets.Application.Interfaces.Repositories;

namespace CinemaTickets.Persistence.Repositories
{
    public class SeatRepository : ISeatRepository
    {
        private readonly CinemaDbContext _context;
        private IMapper _mapper;
        public SeatRepository(CinemaDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<Seat>> GetHallSeats(int hallId)
        {
            var seats = await _context.Seats
                            .Where(s => s.HallId == hallId)
                            .ToListAsync();

            if (!seats.Any())
            {
                throw new ArgumentException("Мест для указанного зала не найдено");
            }

            return _mapper.Map<List<Seat>>(seats);
        }
        
        public async Task<Seat> GetSeatsInfo(int seatId)
        {
            var seat = await _context.Seats
                            .FirstOrDefaultAsync(s => s.Id == seatId) ?? throw new ArgumentException("Данное место не существует");

            return _mapper.Map<Seat>(seat);
        }

        public async Task<SeatAvailability> GetSeatAvailability(int seatId, int seanceId)
        {
            var seatAvailability = await _context.SeatAvailabilities
                    .FirstOrDefaultAsync(s => s.SeatId == seatId && s.SeanceId == seanceId) 
                        ?? throw new ArgumentException("Данное место или сеанс не существует");

            return _mapper.Map<SeatAvailability>(seatAvailability);
        }

        public async Task ChangeSeatStatus(int seatId, int seanceId, bool isAvailable)
        {
            var seatAvailability = await _context.SeatAvailabilities
                           .Where(s => s.SeatId == seatId && s.SeanceId == seanceId)
                           .FirstOrDefaultAsync();

            if (seatAvailability == null)
                throw new ArgumentException("Место не найдено для указанного сеанса");

            seatAvailability.IsAvailable = isAvailable;
            _context.SaveChanges();
        }

        public async Task<decimal> GetSeatPrice(int seatId)
        {
            var price = await _context.Seats
                .Where(s => s.Id == seatId)
                .Select(s => s.Price)
                .FirstOrDefaultAsync();

            if (price == default) 
                throw new InvalidOperationException("Цена для данного места не найдена или не была установлена");

            return price;
        }
    }
}
