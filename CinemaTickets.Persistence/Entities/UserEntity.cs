
namespace CinemaTickets.Persistence.Entities
{
    public class UserEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }

        public ICollection<PaymentEntity> Payments { get; set; }
        public ICollection<TicketEntity> Tickets { get; set; }
    }
}
