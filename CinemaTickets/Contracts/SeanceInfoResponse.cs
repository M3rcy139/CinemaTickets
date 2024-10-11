namespace CinemaTickets.Contracts
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
