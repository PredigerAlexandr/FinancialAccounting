using Microsoft.VisualBasic.CompilerServices;

namespace Domain.Entities;

public class Statistic
{
    /// <summary>
    /// Остаточная сумма всех кредитов
    /// </summary>
    public decimal TotalDebts { get; set; }

    /// <summary>
    /// Сумма, находящаяся во вкладах
    /// </summary>
    public decimal TotalSaves { get; set; }

    /// <summary>
    /// Итоговая сумма выплат за всё время
    /// </summary>
    public decimal TotalPayments { get; set; }

    /// <summary>
    /// Сума, полученная по вкладам (
    /// </summary>
    public decimal TotalProfit { get; set; }
    
    /// <summary>
    /// Дата создания статистики
    /// </summary>
    public DateTime CreateDate { get; set; }

    public Guid UserId { get; set; }
    public required User User { get; set; }
}