
namespace CinemaTickets.Core.Models
{
    public class Payment
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string PaymentType { get; set; }
        public decimal Amount { get; set; }
        public decimal? ChangeGiven { get; set; }
        public DateTime PaymentTime { get; set; }

        public User User { get; set; }

        public ICollection<Ticket> Tickets { get; set; }
    }

}
