namespace CinemaTickets.Contracts.Response
{
    public record SeanceInfoResponse
    (
        int Id,
        string FilmName,
        DateTime StartTime,
        DateTime EndTime,
        int HallId
    );
}
