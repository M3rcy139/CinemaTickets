
namespace CinemaTickets.Persistence.Entities
{
    public class TicketEntity
    {
        public Guid Id { get; set; }
        public int SeatId { get; set; }
        public string HallName { get; set; }
        public string FilmName { get; set; }
        public DateTime FilmStartTime { get; set; }
        public int RowNumber { get; set; }
        public int SeatNumber { get; set; }
        public string UserName { get; set; }
        public string UserSurname { get; set; }
        public DateTime IssueTime { get; set; }
        public decimal Price { get; set; }
        public Guid PaymentId { get; set; }


        public SeatEntity Seat { get; set; }

        public UserEntity User { get; set; }

        public PaymentEntity Payment { get; set; }
    }
}
