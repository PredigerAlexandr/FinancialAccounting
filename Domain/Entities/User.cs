namespace Domain.Entities;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string MiddleName { get; set; }
    public string Surname { get; set; }
    public int? Age { get; set; }
    public int? Salary { get; set; }
    
    public int UserAccessId { get; set; }
    public UserAccess UserAccess { get; set; }
}