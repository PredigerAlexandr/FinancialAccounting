using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Domain.Entities;

public class BankDeposit
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public decimal FullSum { get; set; }
    public decimal CurrentSum { get; set; }
    [DefaultValue(1)]
    public decimal Rate { get; set; }
    
    public Guid UserId { get; set; }
    public User User { get; set; }
    
    public IList<Payoff>? Payoffs { get; set; }
}