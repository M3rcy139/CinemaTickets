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

            return _mapper.Map<List<Hall>>(halls);
        }

        public async Task<List<Seance>> GetSeances()
        {
            var seances = await _context.Seances
                .Select(h => h)
                .ToListAsync();

            return _mapper.Map<List<Seance>>(seances);
        }
    }
}
