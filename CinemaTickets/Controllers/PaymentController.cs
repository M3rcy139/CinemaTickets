using CinemaTickets.Application.Interfaces.Services;
using CinemaTickets.Core.Models;
using CinemaTickets.Persistence;
using Microsoft.AspNetCore.Mvc;
using CinemaTickets.Contracts.Response;
using CinemaTickets.Contracts.Request;

namespace CinemaTickets.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        [HttpPost("pay")]
        public async Task<IActionResult> ProcessPayment([FromBody] PaymentRequest paymentRequest, ISeatService seatService,
            IPaymentService paymentService)
        {
            try
            {
                var user = new User
                {
                    Id = Guid.NewGuid(),
                    Name = paymentRequest.Name,
                    Surname = paymentRequest.Surname,
                    Email = paymentRequest.Email,
                };
 
                var payment = await paymentService.ProcessPayment
                    (user, paymentRequest.AmountPaid, paymentRequest.PaymentType, paymentRequest.SeatId,
                        paymentRequest.SeanceId);

                var response = new PaymentResponse
                (
                    payment.Id,
                    payment.SeatId,
                    payment.PaymentType,
                    payment.Amount,
                    payment.ChangeGiven,
                    payment.PaymentTime

                );

                return Ok(response);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { error = ex.Message });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "An unexpected error occurred.", details = ex.Message });
            }
        }

        [HttpPost("GetTicket")]
        public async Task<IActionResult> GetTicket(Guid paymentId, IPaymentService paymentService)
        {
            try
            {
                var ticket = await paymentService.GetTicket(paymentId);

                var response = new TicketResponse
                (
                    ticket.HallName,
                    ticket.FilmName,
                    ticket.FilmStartTime,
                    ticket.RowNumber,
                    ticket.SeatNumber,
                    ticket.Price,
                    ticket.UserName,
                    ticket.UserSurname,
                    ticket.IssueTime
                );

                return Ok(response);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "An unexpected error occurred.", details = ex.Message });
            }
        }
    }
}

