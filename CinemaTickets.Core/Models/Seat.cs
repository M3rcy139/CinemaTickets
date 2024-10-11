
namespace CinemaTickets.Core.Models
{
    public class Seat
    {
        public int Id { get; set; }
        public int HallId { get; set; }
        public int SeanceId { get; set; }
        public int RowNumber { get; set; }
        public int SeatNumber { get; set; }
        public decimal Price { get; set; }
        public bool IsAvailable { get; set; }

        public Hall Hall { get; set; }
        public Seance Seance { get; set; }
    }

}
