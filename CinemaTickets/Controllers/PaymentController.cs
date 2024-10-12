using CinemaTickets.Application.Interfaces.Services;
using CinemaTickets.Core.Models;
using CinemaTickets.Persistence;
using Microsoft.AspNetCore.Mvc;
using CinemaTickets.Contracts;

namespace CinemaTickets.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly CinemaDbContext _context;

        public PaymentController(CinemaDbContext context)
        {
            _context = context;
        }

        // Прием оплаты
        [HttpPost("pay")]
        public IActionResult ProcessPayment([FromBody] PaymentRequest paymentRequest, ISeatService seatService,
            IPaymentService paymentService)
        {
            var user = new User
            {
                Id = new Guid(),
                Name = paymentRequest.Name,
                Surname = paymentRequest.Surname,
                Email = paymentRequest.Email,
            };
 
            var payment = paymentService.ProcessPayment
                (user, paymentRequest.AmountPaid, paymentRequest.PaymentType, paymentRequest.SeatId).Result;

            var response = new PaymentResponse
            (
                payment.Id,
                payment.PaymentType,
                payment.Amount,
                payment.ChangeGiven,
                payment.PaymentTime

            );

            return Ok(response);
        }
    }
}

