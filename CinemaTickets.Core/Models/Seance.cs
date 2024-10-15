
namespace CinemaTickets.Core.Models
{
    public class Seance
    {
        public int Id { get; set; }
        public string FilmName { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int HallId { get; set; }

        public Hall Hall { get; set; }
        public ICollection<SeatAvailability> SeatAvailabilities { get; set; }
    }
}
