namespace Domain.Entities;

public class Payoff
{
    public Guid Id { get; set; }
    public decimal Amount { get; set; }
    public DateTime DateCreate { get; set; }
    public Guid BankDepositId { get; set; }
    public required BankDeposit BankDeposit { get; set; }
}