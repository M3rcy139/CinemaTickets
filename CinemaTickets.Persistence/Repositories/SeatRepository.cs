﻿using AutoMapper;
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
        public async Task<List<Seat>> GetHallSeats(int hallId, int seanceId)
        {
            var seats = await _context.Seats
                            .Where(s => s.HallId == hallId && s.SeanceId == seanceId)
                            .ToListAsync();

            return _mapper.Map<List<Seat>>(seats);
        }
        
        public async Task<Seat> GetSeatsInfo(int seatId)
        {
            var seat = await _context.Seats
                            .FirstOrDefaultAsync(s => s.Id == seatId);

            return _mapper.Map<Seat>(seat);
        }

        public async Task ChangeSeatStatus(int seatId, bool isAvailable)
        {
            var seat = await _context.Seats
                           .Where(s => s.Id == seatId)
                           .FirstOrDefaultAsync();

            if (seat == null)
                throw new Exception("Место не найдено для указанного сеанса");

            seat.IsAvailable = isAvailable;
            _context.SaveChanges();
        }
    }
}
