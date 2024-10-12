

using CinemaTickets.Core.Models;

namespace CinemaTickets.Application.Interfaces.Services
{
    public interface ISeatService
    {
        Task<List<Seat>> GetHallSeats(int hallId, int seanceId);
        Task<Seat> GetSeatsInfo(int seatId);
        Task ChangeSeatStatus(int seatId, bool isAvailable);
        Task<decimal> GetSeatPrice(int seatId);
    }
}
