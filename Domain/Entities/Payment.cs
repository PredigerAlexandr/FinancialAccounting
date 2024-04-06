namespace Domain.Entities;

public class Payment
{
    public Guid Id { get; set; }
    public decimal Amount { get; set; }
    public DateTime DateCreate { get; set; }
    public Guid DebtId { get; set; }
    public required Debt Debt { get; set; }
}