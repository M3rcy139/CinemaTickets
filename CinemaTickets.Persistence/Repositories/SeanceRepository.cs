using AutoMapper;
using CinemaTickets.Application.Interfaces.Repositories;
using CinemaTickets.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace CinemaTickets.Persistence.Repositories
{
    public class SeanceRepository : ISeanceRepository
    {
        private readonly CinemaDbContext _context;
        private IMapper _mapper;
        public SeanceRepository(CinemaDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<Hall>> GetHalls()
        {
            var halls = await _context.Halls
                .Select(h => h)
                .ToListAsync();

            if (!halls.Any())
                throw new ArgumentException("Ни одного зала не найдено");

            return _mapper.Map<List<Hall>>(halls);
        }

        public async Task<List<Seance>> GetSeances(int hallId)
        {
            var seances = await _context.Seances
                .Where(s => s.HallId == hallId)
                .ToListAsync();

            if (!seances.Any()) throw new ArgumentException($"Сеансов для данного зала ({hallId}) не найдено");

            return _mapper.Map<List<Seance>>(seances);
        }
    }
}
