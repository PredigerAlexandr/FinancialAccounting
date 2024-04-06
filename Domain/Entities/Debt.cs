using Domain.Models.Enums;

namespace Domain.Entities;

public class Debt
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public decimal FullSum { get; set; }
    public decimal CurrentSum { get; set; }
    public decimal Rate { get; set; }
    public DebtType Type { get; set; }

    public Guid UserId { get; set; }
    public required User User { get; set; }
    
    public IList<Payment>? Payments { get; set; }
}