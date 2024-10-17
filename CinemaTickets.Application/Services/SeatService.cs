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
        public async Task<List<Seat>> GetHallSeats(int hallId)
        {
            var seats = await _seatRepository.GetHallSeats(hallId);

            return seats;
        }
        public async Task<Seat> GetSeatsInfo(int seatId)
        {
            var seat = await _seatRepository.GetSeatsInfo(seatId);

            return seat;
        }

        public async Task<SeatAvailability> GetSeatAvailability(int seatId, int seanceId)
        {
            var seatAvailability = await _seatRepository.GetSeatAvailability(seatId, seanceId);

            return seatAvailability;
        }
        public async Task ChangeSeatStatus(int seatId, int seanceId, bool isAvailable)
        {
            await _seatRepository.ChangeSeatStatus(seatId, seanceId, isAvailable);
        }

        public async Task<decimal> GetSeatPrice(int seatId)
        {
            var price = await _seatRepository.GetSeatPrice(seatId);

            return price;
        }
    }
}
