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

        [HttpPost("GetTicket")]
        public async Task<IActionResult> GetTicket(Guid paymentId, IPaymentService paymentService)
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
    }
}

