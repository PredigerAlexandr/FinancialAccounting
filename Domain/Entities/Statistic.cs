using Microsoft.VisualBasic.CompilerServices;

namespace Domain.Entities;

public class Statistic
{
    /// <summary>
    /// Id объекта
    /// </summary>
    public Guid Id { get; set; }
    /// <summary>
    /// Остаточная сумма всех кредитов
    /// </summary>
    public decimal TotalDebts { get; set; }

    /// <summary>
    /// Сумма, находящаяся во вкладах
    /// </summary>
    public decimal TotalDeposits { get; set; }

    /// <summary>
    /// Итоговая сумма выплат за всё время
    /// </summary>
    public decimal TotalPayments { get; set; }

    /// <summary>
    /// Сума, полученная по вкладам (
    /// </summary>
    public decimal TotalPayoffs { get; set; }
    
    /// <summary>
    /// Дата создания статистики
    /// </summary>
    public DateTime CreateDate { get; set; }

    public Guid UserId { get; set; }
    public required User User { get; set; }
}