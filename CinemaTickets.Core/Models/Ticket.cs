

namespace CinemaTickets.Core.Models
{
    public class Ticket
    {
        public Guid Id { get; set; }
        public int SeanceId { get; set; }
        public int SeatId { get; set; }
        public Guid UserId { get; set; }
        public DateTime IssueTime { get; set; }
        public Guid PaymentId { get; set; }

        public Seance Seance { get; set; }

        public Seat Seat { get; set; }

        public User User { get; set; }

        public Payment Payment { get; set; }
    }

}
