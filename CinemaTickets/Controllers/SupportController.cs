using Microsoft.AspNetCore.Mvc;
using CinemaTickets.Application.Interfaces.Repositories;

namespace CinemaTickets.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupportController : ControllerBase
    {
        [HttpPost("report-issue")]
        public async Task<IActionResult> ReportIssue(string message, ISupportRepository supportRepository)
        {
            await supportRepository.ReportIssue(message);

            return Ok("Сообщение успешно отправлено");
        }
    }
}
