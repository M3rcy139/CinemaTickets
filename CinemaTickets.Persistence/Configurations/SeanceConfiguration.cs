using CinemaTickets.Persistence.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace CinemaTickets.Persistence.Configurations
{
    public class SeanceConfiguration : IEntityTypeConfiguration<SeanceEntity>
    {
        public void Configure(EntityTypeBuilder<SeanceEntity> builder)
        {
            builder.ToTable("Seances");

            builder.HasKey(s => s.Id);

            builder.Property(s => s.FilmName)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(s => s.StartTime)
                .IsRequired();

            builder.Property(s => s.EndTime)
                .IsRequired();

            builder.HasOne(s => s.Hall)
                .WithMany(h => h.Seances)
                .HasForeignKey(s => s.HallId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
