
namespace CinemaTickets.Persistence.Entities
{
    public class SeatAvailabilityEntity
    {
        public Guid Id { get; set; }
        public int SeatId { get; set; }
        public int SeanceId { get; set; }
        public Guid? PaymentId { get; set; }
        public bool IsAvailable { get; set; } = true;

        public PaymentEntity Payment { get; set; }
        public SeatEntity Seat { get; set; }
        public SeanceEntity Seance { get; set; }
    }
}
