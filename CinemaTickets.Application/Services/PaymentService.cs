using CinemaTickets.Application.Interfaces.Repositories;
using CinemaTickets.Application.Interfaces.Services;
using CinemaTickets.Core.Models;

namespace CinemaTickets.Application.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;
        public PaymentService(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }
        
        public async Task<Payment> ProcessPayment(User user, decimal amountPaid, string paymentType, int seatId)
        {
            var payment = await _paymentRepository.ProcessPayment(user, amountPaid, paymentType, seatId);

            return payment;
        }

        public async Task<Ticket> GetTicket(Guid paymentId)
        {
            var ticket = await _paymentRepository.GetTicket(paymentId);

            return ticket;
        }
    }
}
