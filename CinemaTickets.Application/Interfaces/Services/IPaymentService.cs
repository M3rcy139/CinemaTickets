using CinemaTickets.Core.Models;


namespace CinemaTickets.Application.Interfaces.Services
{
    public interface IPaymentService
    {
        Task<Payment> ProcessPayment(User user, decimal amountPaid, string paymentType, int seatId);
    }
}
