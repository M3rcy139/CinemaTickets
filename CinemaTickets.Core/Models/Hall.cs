
namespace CinemaTickets.Core.Models
{
    public class Hall
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Capacity { get; set; }
        public int Rows { get; set; }
        public int Columns { get; set; }


        public ICollection<Seat> Seats { get; set; }
        public ICollection<Seance> Seances { get; set; }
    }
}
