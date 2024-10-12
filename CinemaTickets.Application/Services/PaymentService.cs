using CinemaTickets.Application.Interfaces.Repositories;
using CinemaTickets.Application.Interfaces.Services;
using CinemaTickets.Core.Models;

namespace CinemaTickets.Application.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly ISeatService _seatService;
        public PaymentService(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        public async Task<Payment> ProcessPayment(User user, decimal amountPaid, string paymentType, int seatId)
        {
            var totalCost = _seatService.GetSeatPrice(seatId).Result;

            if (totalCost > amountPaid)
                throw new InvalidOperationException("Недостаточно средств");

            decimal changeGiven = amountPaid - totalCost;

            var payment = await _paymentRepository.ProcessPayment(user, amountPaid, paymentType, changeGiven);

            return payment;
        }
    }
}
