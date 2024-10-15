using AutoMapper;
using CinemaTickets.Application.Interfaces.Repositories;
using CinemaTickets.Persistence.Entities;

namespace CinemaTickets.Persistence.Repositories
{
    public class SupportRepository : ISupportRepository
    {
        private readonly CinemaDbContext _context;
        private IMapper _mapper;
        public SupportRepository(CinemaDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task ReportIssue(string message)
        {
            var issueReportEntity = new IssueReportEntity()
            {
                Message = message,
                MessageTime = DateTime.UtcNow,
            };
            await _context.IssueReports.AddAsync(issueReportEntity);
            await _context.SaveChangesAsync();
        }
    }
}
