namespace CinemaTickets.Contracts
{
    public record PaymentRequest
    (
        string Name,
        string Surname,
        string Email,
        string PaymentType,
        decimal AmountPaid,
        int SeatId
    );
}
