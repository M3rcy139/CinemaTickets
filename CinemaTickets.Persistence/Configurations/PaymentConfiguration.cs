using CinemaTickets.Persistence.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace CinemaTickets.Persistence.Configurations
{
    public class PaymentConfiguration : IEntityTypeConfiguration<PaymentEntity>
    {
        public void Configure(EntityTypeBuilder<PaymentEntity> builder)
        {
            builder.ToTable("Payments");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.PaymentType)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(p => p.Amount)
                .IsRequired()
                .HasColumnType("decimal(18, 2)");

            builder.Property(p => p.ChangeGiven)
                .HasColumnType("decimal(18, 2)");

            builder.Property(p => p.PaymentTime)
                .IsRequired();

            builder.HasOne(p => p.User)
                .WithMany(u => u.Payments)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(p => p.Seats)
                   .WithOne()  
                   .HasForeignKey(s => s.Id)  
                   .OnDelete(DeleteBehavior.Restrict); 

            // Связь с билетами (TicketEntity)
            builder.HasMany(p => p.Tickets)
                   .WithOne(t => t.Payment)  
                   .HasForeignKey(t => t.PaymentId)
                   .OnDelete(DeleteBehavior.Cascade);  
        }
    }
}
