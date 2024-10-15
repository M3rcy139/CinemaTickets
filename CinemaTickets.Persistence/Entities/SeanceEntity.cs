
namespace CinemaTickets.Persistence.Entities
{
    public class SeanceEntity
    {
        public int Id { get; set; }
        public string FilmName { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int HallId { get; set; }

        public HallEntity Hall { get; set; }
        public ICollection<SeatAvailabilityEntity> SeatAvailabilities { get; set; }
    }
}
