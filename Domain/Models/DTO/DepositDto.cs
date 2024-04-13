namespace Domain.Models.DTO;

public record DepositDto
{
    public string Name { get; init; }
    public decimal FullSum { get; init; }
    public bool Capitalization { get; init; }
    public decimal Rate { get; init; }
    public DateTime DateStart { get; init; }
}