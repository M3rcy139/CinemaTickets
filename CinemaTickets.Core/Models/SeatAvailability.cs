namespace CinemaTickets.Core.Models
{
    public class SeatAvailability
    {
        public Guid Id { get; set; }
        public int SeatId { get; set; }
        public int SeanceId { get; set; }
        public Guid? PaymentId { get; set; }
        public bool IsAvailable { get; set; } = true;

        public Payment Payment { get; set; }
        public Seat Seat { get; set; }
        public Seance Seance { get; set; }
    }
}
