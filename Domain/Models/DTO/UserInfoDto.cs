namespace Domain.Models.DTO;

public class UserInfoDto
{
    public string Name { get; set; }
    public string MiddleName { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public int? Age { get; set; }
    public int? Salary { get; set; }
    public int? JkhSummer { get; set; }
    public int? JkhWinter { get; set; }
    public bool? IsAuto { get; set; }
    public int? AnotherPayments { get; set; }
}