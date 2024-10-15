using CinemaTickets.Persistence.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace CinemaTickets.Persistence.Configurations
{
    public class IssueReportConfiguration : IEntityTypeConfiguration<IssueReportEntity>
    {
        public void Configure(EntityTypeBuilder<IssueReportEntity> builder)
        {
            builder.ToTable("IssueReports");

            builder.HasKey(i => i.Id);

            builder.Property(i => i.Message)
                .IsRequired()
                .HasMaxLength(250);

            builder.Property(i => i.MessageTime)
                .IsRequired();
        }
    }
}
