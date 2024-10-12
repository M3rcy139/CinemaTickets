using AutoMapper;
using CinemaTickets.Persistence.Entities;
using CinemaTickets.Core.Models;
using CinemaTickets.Persistence.Repositories;
using CinemaTickets.Application.Interfaces.Repositories;
using CinemaTickets.Application.Interfaces.Services;

namespace CinemaTickets.Persistence.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly CinemaDbContext _context;
        private IMapper _mapper;
        private ISeatService _seatService;
        public PaymentRepository(CinemaDbContext context, IMapper mapper, ISeatService seatService)
        {
            _context = context;
            _mapper = mapper;
            _seatService = seatService;
        }
        public async Task<Payment> ProcessPayment(User user, decimal amointPaid, string paymentType, decimal changeGiven)
        {
            var userEntity = _mapper.Map<UserEntity>(user);

            var paymentEntity = new PaymentEntity
            {
                Id = new Guid(),
                UserId = user.Id,
                Amount = amointPaid,
                PaymentType = paymentType,
                PaymentTime = DateTime.Now,
                ChangeGiven = changeGiven
            };

            userEntity.Payments.Add(paymentEntity);

            await _context.Users.AddAsync(userEntity);
            await _context.Payments.AddAsync(paymentEntity);
            await _context.SaveChangesAsync();

            return _mapper.Map<Payment>(paymentEntity);
        }
    }
}
