using Domain.Models.Enums;

namespace Domain.Entities;

public class Loan
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public decimal FullSum { get; set; }
    public decimal CurrentSum { get; set; }
    public decimal Rate { get; set; }
    public LoanType Type { get; set; }
    public User User { get; set; }
}