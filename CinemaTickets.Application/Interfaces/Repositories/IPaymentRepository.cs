

using CinemaTickets.Core.Models;

namespace CinemaTickets.Application.Interfaces.Repositories
{
    public interface IPaymentRepository
    {
        Task<Payment> ProcessPayment(User user, decimal amountPaid, string paymentType, decimal changeGiven);
    }
}
