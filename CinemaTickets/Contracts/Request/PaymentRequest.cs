namespace CinemaTickets.Contracts.Request
{
    public record PaymentRequest
    (
        string Name,
        string Surname,
        string Email,
        string PaymentType,
        decimal AmountPaid,
        int SeatId,
        int SeanceId
    );
}
