namespace Domain.Entities;

public class User
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string MiddleName { get; set; }
    public string Surname { get; set; }
    public int? Age { get; set; }
    public int? Salary { get; set; }
    
    public string Email { get; set; }
    public string Password { get; set; }

    public List<Loan>? Loans { get; set; }
    public List<BankDeposit>? BankDeposits { get; set; }
}