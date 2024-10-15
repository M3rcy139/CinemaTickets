
namespace CinemaTickets.Core.Models
{
    public class Seat
    {
        public int Id { get; set; }
        public int HallId { get; set; }
        public int RowNumber { get; set; }
        public int SeatNumber { get; set; }
        public decimal Price { get; set; }

        public Hall Hall { get; set; }
        public ICollection<SeatAvailability> SeatAvailabilities { get; set; }
    }

}
