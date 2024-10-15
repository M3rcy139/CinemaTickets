using Microsoft.Extensions.DependencyInjection;
using CinemaTickets.Application.Interfaces.Services;
using CinemaTickets.Application.Services;

namespace CinemaTickets.Application
{
    public static class ApplicationExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IPaymentService, PaymentService>();
            services.AddScoped<ISeanceService, SeanceService>();
            services.AddScoped<ISeatService, SeatService>();

            return services;
        }
    }
}
