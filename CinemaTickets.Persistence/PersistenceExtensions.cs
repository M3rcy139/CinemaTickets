using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using CinemaTickets.Application.Interfaces.Services;
using CinemaTickets.Application.Services;
using CinemaTickets.Application.Interfaces.Repositories;
using CinemaTickets.Persistence.Repositories;

namespace CinemaTickets.Persistence
{
    public static class PersistenceExtensions
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services
                , IConfiguration configuration)
        {
            services.AddDbContext<CinemaDbContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString(nameof(CinemaDbContext)));
            });

            services.AddScoped<IPaymentRepository, PaymentRepository>();
            services.AddScoped<ISeanceRepository, SeanceRepository>();
            services.AddScoped<ISeatRepository, SeatRepository>();
            services.AddScoped<ISupportRepository, SupportRepository>();

            return services;
        }
    }
}
