﻿

using CinemaTickets.Core.Models;

namespace CinemaTickets.Application.Interfaces.Services
{
    public interface ISeatService
    {
        Task<List<Seat>> GetHallSeats(int hallId);
        Task<Seat> GetSeatsInfo(int seatId);
        Task<SeatAvailability> GetSeatAvailability(int seatId, int seanceId);
        Task ChangeSeatStatus(int seatId, int seanceId, bool isAvailable);
        Task<decimal> GetSeatPrice(int seatId);
    }
}
