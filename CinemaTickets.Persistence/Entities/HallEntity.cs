
namespace CinemaTickets.Persistence.Entities
{
    public class HallEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Capacity { get; set; }

        public ICollection<SeatEntity> Seats { get; set; }
        public ICollection<SeanceEntity> Seances { get; set; }
    }
}
