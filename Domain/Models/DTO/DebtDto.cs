namespace Application.Common.Models;

public class DebtDto
{
    public string Name { get; set; }
    public decimal FullSum { get; set; }
    public decimal CurrentSum { get; set; }
    public decimal? Rate { get; set; }
    public string Type { get; set; }
    public string UserEmail { get; set; }
}