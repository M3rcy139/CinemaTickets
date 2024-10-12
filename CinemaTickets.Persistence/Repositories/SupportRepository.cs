using AutoMapper;
using CinemaTickets.Persistence.Entities;

namespace CinemaTickets.Persistence.Repositories
{
    public class SupportRepository
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
                MessageTime = DateTime.Now,
            };
            await _context.IssueReports.AddAsync(issueReportEntity);
            await _context.SaveChangesAsync();
        }
    }
}
