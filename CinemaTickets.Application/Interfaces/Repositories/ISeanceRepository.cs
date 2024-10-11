using CinemaTickets.Core.Models;

namespace CinemaTickets.Application.Interfaces.Repositories
{
    public interface ISeanceRepository
    {
        Task<List<Hall>> GetHalls();
    }
}
