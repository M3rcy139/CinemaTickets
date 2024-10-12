using CinemaTickets.Persistence;
using Microsoft.AspNetCore.Mvc;
using CinemaTickets.Application.Interfaces.Services;
using CinemaTickets.Contracts;

namespace CinemaTickets.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeanceController : ControllerBase
    {
        private readonly CinemaDbContext _context;

        public SeanceController(CinemaDbContext context)
        {
            _context = context;
        }

        [HttpGet("get-halls/{hallId}/seance/{seanceId}")]
        public async Task<IActionResult> GetHalls(ISeanceService seanceService)
        {
            var halls = await seanceService.GetHalls();

            var response = halls.Select(h => h.Id).ToList();

            return Ok(response);
        }

        [HttpGet("get-seances/hall/{hallId}/seance/{seanceId}")]
        public async Task<IActionResult> GetSeances(int hallId, ISeanceService seanceService)
        {
            var seances = await seanceService.GetSeances();

            var response = seances
                .Select(s => new SeanceInfoResponse
                (
                    s.Id,
                    s.FilmName,
                    s.StartTime,
                    s.EndTime,
                    s.HallId
                ));

            return Ok(response);
        }
    }
}
