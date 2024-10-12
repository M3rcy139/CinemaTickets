using CinemaTickets.Persistence;
using Microsoft.AspNetCore.Mvc;
using CinemaTickets.Application.Interfaces.Repositories;

namespace CinemaTickets.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupportController : ControllerBase
    {
        private readonly CinemaDbContext _context;

        public SupportController(CinemaDbContext context)
        {
            _context = context;
        }

        [HttpPost("report-issue")]
        public async Task<IActionResult> ReportIssue(string message, ISupportRepository supportRepository)
        {
            supportRepository.ReportIssue(message);

            return Ok("Сообщение успешно отправлено");
        }
    }
}
