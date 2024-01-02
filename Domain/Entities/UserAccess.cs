namespace Domain.Entities;

public class UserAccess
{
    public int Id { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }
    
    public int UserId { get; set; }
    public UserAccess User { get; set; }
}