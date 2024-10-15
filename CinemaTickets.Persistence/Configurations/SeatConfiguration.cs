using CinemaTickets.Persistence.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace CinemaTickets.Persistence.Configurations
{
    public class SeatConfiguration : IEntityTypeConfiguration<SeatEntity>
    {
        public void Configure(EntityTypeBuilder<SeatEntity> builder)
        {
            builder.ToTable("Seats");

            builder.HasKey(s => s.Id);

            builder.Property(s => s.RowNumber)
                .IsRequired();

            builder.Property(s => s.SeatNumber)
                .IsRequired();

            builder.Property(s => s.Price)
                .IsRequired()
                .HasColumnType("decimal(18, 2)");

            builder.HasOne(s => s.Hall)
                .WithMany(h => h.Seats)
                .HasForeignKey(s => s.HallId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }

}
