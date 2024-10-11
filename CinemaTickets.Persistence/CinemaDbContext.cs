using CinemaTickets.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace CinemaTickets.Persistence
{
    public class CinemaDbContext(DbContextOptions<CinemaDbContext> options) : DbContext(options)
    {
        public DbSet<HallEntity> Halls { get; set; }
        public DbSet<PaymentEntity> Payments { get; set; }
        public DbSet<SeanceEntity> Seances { get; set; }
        public DbSet<SeatEntity> Seats { get; set; }
        public DbSet<TicketEntity> Tickets { get; set; }
        public DbSet<UserEntity> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CinemaDbContext).Assembly); //Проверить в гпт
        }
    }
}
