namespace CinemaTickets.Contracts.Response
{
    public record SeatInfoResponse
    (
        int Id,
        int RowNumber,
        int SeatNumber,
        decimal Price,
        bool IsAvailable
    );
}
