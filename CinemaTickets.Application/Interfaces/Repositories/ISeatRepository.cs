

using CinemaTickets.Core.Models;

namespace CinemaTickets.Application.Interfaces.Repositories
{
    public interface ISeatRepository
    {
        Task<List<Seat>> GetHallSeats(int hallId, int seanceId);
        Task<Seat> GetSeatsInfo(int seatId);
        Task ChangeSeatStatus(int seatId, bool isAvailable);
    }
}
