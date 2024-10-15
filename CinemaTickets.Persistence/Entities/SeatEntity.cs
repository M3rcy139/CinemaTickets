
namespace CinemaTickets.Persistence.Entities
{
    public class SeatEntity
    {
        public int Id { get; set; }
        public int HallId { get; set; }
        public int RowNumber { get; set; }
        public int SeatNumber { get; set; }
        public decimal Price { get; set; }

        public HallEntity Hall { get; set; }
        public ICollection<SeatAvailabilityEntity> SeatAvailabilities { get; set; }
    }
}
