namespace CinemaTickets.Contracts
{
    public record PaymentResponse
    (
        Guid Id,
        string PaymentType,
        decimal Amount,
        decimal? ChangeGiven,
        DateTime PaymentTime 
    );
}
