using CinemaTickets.Core.Models;

namespace CinemaTickets.Application.Interfaces.Services
{
    public interface ISeanceService
    {
        Task<List<Hall>> GetHalls();
        Task<List<Seance>> GetSeances();
    }
}
