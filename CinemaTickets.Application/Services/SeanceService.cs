using CinemaTickets.Application.Interfaces.Repositories;
using CinemaTickets.Application.Interfaces.Services;
using CinemaTickets.Core.Models;


namespace CinemaTickets.Application.Services
{
    public class SeanceService : ISeanceService
    {
        private readonly ISeanceRepository _seanceRepository;

        public SeanceService(ISeanceRepository seanceRepository)
        {
            _seanceRepository = seanceRepository;
        }

        public async Task<List<Hall>> GetHalls()
        {
            var halls = await _seanceRepository.GetHalls();

            return halls;
        }

        public async Task<List<Seance>> GetSeances(int hallId)
        {
            var seances = await _seanceRepository.GetSeances(hallId);

            return seances;
        }
    }
}
