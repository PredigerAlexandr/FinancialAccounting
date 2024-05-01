namespace Domain.Models.DTO;

public class DebtDto
{
    public string Name { get; set; }
    public decimal FullSum { get; set; }
    public decimal CurrentSum { get; set; }
    public decimal? Rate { get; set; }
    public string Type { get; set; }
    public string UserEmail { get; set; }
    public DateTime DateStart { get; set; }
    public int MonthsTotal { get; set; }
    public List<Object> OffersDepositToDebt { get; set; } = new List<Object>();
}