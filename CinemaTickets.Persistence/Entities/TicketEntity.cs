
namespace CinemaTickets.Persistence.Entities
{
    public class TicketEntity
    {
        public Guid Id { get; set; }
        public int SeanceId { get; set; }
        public int SeatId { get; set; }
        public Guid UserId { get; set; }
        public DateTime IssueTime { get; set; }
        public Guid PaymentId { get; set; }

        public SeanceEntity Seance { get; set; }

        public SeatEntity Seat { get; set; }

        public UserEntity User { get; set; }

        public PaymentEntity Payment { get; set; }
    }
}
