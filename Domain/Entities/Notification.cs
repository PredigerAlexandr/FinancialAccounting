namespace Domain.Entities;

public class Notification
{
    public Guid Id { get; set; }
    public string? IdInMessage { get; set; }
    public string Type { get; set; }
    public string Description { get; set; }
    public string Title { get; set; }
    public string Url { get; set; }
    public bool Read { get; set; } = false;
    public DateTime Date { get; set; }
    
    public Guid UserId { get; set; }
    public User User { get; set; }
    
}