using Microsoft.AspNetCore.Mvc;
using CinemaTickets.Application.Interfaces.Repositories;

namespace CinemaTickets.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupportController : ControllerBase
    {
        [HttpPost("report-issue/{message}")]
        public async Task<IActionResult> ReportIssue([FromBody]string message, ISupportRepository supportRepository)
        {
            try
            {
                await supportRepository.ReportIssue(message);

                return Ok("Сообщение успешно отправлено");
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "An unexpected error occurred.", details = ex.Message });
            }
        }
    }
}
