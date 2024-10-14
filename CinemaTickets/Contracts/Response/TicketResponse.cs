namespace CinemaTickets.Contracts.Response
{
    public record TicketResponse
    (
        string HallName,
        string FilmName,
        DateTime FilmStartTime,
        int RowNumber,
        int SeatNumber,
        decimal Price,
        string UserName,
        string UserSurname,
        DateTime IssueTime
    );
}
