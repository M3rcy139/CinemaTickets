using CinemaTickets.Application.Interfaces.Repositories;
using CinemaTickets.Application.Interfaces.Services;
using CinemaTickets.Core.Models;

namespace CinemaTickets.Application.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly ISeatService _seatService;
        public PaymentService(IPaymentRepository paymentRepository, ISeatService seatService)
        {
            _paymentRepository = paymentRepository;
            _seatService = seatService;
        }
        
        public async Task<Payment> ProcessPayment(User user, decimal amountPaid, string paymentType, int seatId)
        {
            var payment = await _paymentRepository.ProcessPayment(user, amountPaid, paymentType, seatId);

            await _seatService.ChangeSeatStatus(seatId, false);

            return payment;
        }

        public async Task<Ticket> GetTicket(Guid paymentId)
        {
            var ticket = await _paymentRepository.GetTicket(paymentId);

            return ticket;
        }
    }
}
