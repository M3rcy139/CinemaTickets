

using CinemaTickets.Core.Models;

namespace CinemaTickets.Application.Interfaces.Repositories
{
    public interface ISeatRepository
    {
        Task<List<Seat>> GetHallSeats(int hallId);
        Task<Seat> GetSeatsInfo(int seatId, int seanceId);
        Task<SeatAvailability> GetSeatAvailability(int seatId, int seanceId);
        Task ChangeSeatStatus(int seatId, bool isAvailable);
        Task<decimal> GetSeatPrice(int seatId);
    }
}
