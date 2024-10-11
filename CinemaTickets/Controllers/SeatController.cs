using CinemaTickets.Application.Interfaces.Services;
using CinemaTickets.Contracts;
using CinemaTickets.Core.Models;
using CinemaTickets.Persistence;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class SeatController : ControllerBase
{
    private readonly CinemaDbContext _context;

    public SeatController(CinemaDbContext context)
    {
        _context = context;
    }

    [HttpGet("allseats/hall/{hallId}/seance/{seanceId}")]
    public async Task<IActionResult> GetHallSeats(int hallId, int seanceId, ISeatService seatService)
    {
        var seats = await seatService.GetHallSeats(hallId, seanceId);

        var response = seats.Select(s => s.Id);

        return Ok(response);
    }

    [HttpGet("seat-info/{seatId}")]
    public async Task<IActionResult> GetSeatsInfo(int seatId, ISeatService seatService)
    {
        var seat = await seatService.GetSeatsInfo(seatId);

        var response = new SeatInfoResponse
        (
            seat.Id, 
            seat.RowNumber, 
            seat.SeatNumber, 
            seat.Price, 
            seat.IsAvailable
        );

        return Ok(response);
    }

    [HttpPut("seance/{seanceId}/seat/{seatId}/change-status")]
    public async Task<IActionResult> ChangeSeatStatus(int seatId, bool isAvailable, ISeatService seatService)
    {
        await seatService.ChangeSeatStatus(seatId, isAvailable);

        return Ok();
    }
}
