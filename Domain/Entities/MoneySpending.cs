namespace Domain.Entities;

public class MoneySpending
{
    public Guid Id { get; set; }
    public string Type { get; set; }
    public int Amount { get; set; }
    
    public Guid UserId { get; set; }
    public User User { get; set; }
}