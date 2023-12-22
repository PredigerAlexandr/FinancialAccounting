namespace Domain.Entities;

public class Loan
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal FullSum { get; set; }
    public decimal CurrentSum { get; set; }
    public decimal Rate { get; set; }
    
    public int UserId { get; set; }
    public User User { get; set; }
}