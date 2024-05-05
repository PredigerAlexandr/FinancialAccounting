namespace Domain.Entities;

public class User
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string MiddleName { get; set; }
    public string Surname { get; set; }
    public int? Age { get; set; }
    public int? Salary { get; set; }
    public string? City { get; set; }
    public string? ProfileWork { get; set; }
    public int? JkhSummer { get; set; }
    public int? JkhWinter { get; set; }
    public bool? IsAuto { get; set; }
    public int? AnotherPayments { get; set; }

    public string Email { get; set; }
    public string Password { get; set; }

    public List<Debt> Debts { get; set; } = new List<Debt>();
    public List<BankDeposit>? BankDeposits { get; set; }
    public List<Statistic>? Statistics { get; set; }
    public List<MoneySpending>? MoneySpendings { get; set; }

    public List<Notification> Notifications { get; set; } = new List<Notification>();
}