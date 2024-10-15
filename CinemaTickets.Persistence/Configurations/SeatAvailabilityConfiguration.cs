using CinemaTickets.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CinemaTickets.Persistence.Configurations
{
    public class SeatAvailabilityConfiguration
    {
        public void Configure(EntityTypeBuilder<SeatAvailabilityEntity> builder)
        {
            builder.ToTable("SeatAvailability");

            builder.HasKey(sa => sa.Id);

            builder.HasOne(sa => sa.Seat)
                   .WithMany(s => s.SeatAvailabilities)
                   .HasForeignKey(sa => sa.SeatId)
                   .OnDelete(DeleteBehavior.Cascade); 

            builder.HasOne(sa => sa.Seance)
                   .WithMany(se => se.SeatAvailabilities)
                   .HasForeignKey(sa => sa.SeanceId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(sa => sa.Payment)
                  .WithMany(p => p.SeatAvailabilities) 
                  .HasForeignKey(sa => sa.PaymentId) 
                  .OnDelete(DeleteBehavior.SetNull);

            builder.Property(sa => sa.IsAvailable)
                   .IsRequired();
        }
    }
}
