using CinemaTickets.Application.Interfaces.Repositories;
using CinemaTickets.Application.Interfaces.Services;
using CinemaTickets.Core.Models;

namespace CinemaTickets.Application.Services
{
    public class SeatService : ISeatService
    {
        private readonly ISeatRepository _seatRepository;
        public SeatService(ISeatRepository seatRepository)
        {
            _seatRepository = seatRepository;
        }
        public async Task<List<Seat>> GetHallSeats(int hallId, int seanceId)
        {
            var seats = await _seatRepository.GetHallSeats(hallId, seanceId);

            return seats;
        }
        public async Task<Seat> GetSeatsInfo(int seatId)
        {
            var seat = await _seatRepository.GetSeatsInfo(seatId);

            return seat;
        }
        public async Task ChangeSeatStatus(int seatId, bool isAvailable)
        {
            await _seatRepository.ChangeSeatStatus(seatId, isAvailable);
        }
    }
}
