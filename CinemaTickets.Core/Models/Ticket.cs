

namespace CinemaTickets.Core.Models
{
    public class Ticket
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


        public Seat Seat { get; set; }

        public User User { get; set; }

        public Payment Payment { get; set; }
    }

}
