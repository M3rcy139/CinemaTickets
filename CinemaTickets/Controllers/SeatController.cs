using CinemaTickets.Application.Interfaces.Services;
using CinemaTickets.Contracts.Response;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class SeatController : ControllerBase
{
    [HttpGet("allseats/hall/{hallId}")]
    public async Task<IActionResult> GetHallSeats(int hallId, ISeatService seatService)
    {
        var seats = await seatService.GetHallSeats(hallId);

        var response = seats.Select(s => s.Id);

        return Ok(response);
    }

    [HttpGet("seat-info/seat{seatId}/seance/{seanceId}")]
    public async Task<IActionResult> GetSeatsInfo(int seatId, int seanceId, ISeatService seatService)
    {
        var seat = await seatService.GetSeatsInfo(seatId, seanceId);

        var seatAvailability = await seatService.GetSeatAvailability(seatId, seanceId);

        var response = new SeatInfoResponse
        (
            seat.Id, 
            seat.RowNumber, 
            seat.SeatNumber, 
            seat.Price, 
            seatAvailability.IsAvailable
        );

        return Ok(response);
    }
}
