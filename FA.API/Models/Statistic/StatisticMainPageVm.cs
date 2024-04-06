namespace CreditAPI.Models.Statistic;

public record StatisticMainPageVm
{
    /// <summary>
    /// Остаточная сумма всех кредитов
    /// </summary>
    public decimal TotalDebts { get; init; }
    
    /// <summary>
    /// Сумма, находящаяся во вкладах
    /// </summary>
    public decimal TotalSaves{ get; init; }
    
    /// <summary>
    /// Итоговая сумма выплат за всё время
    /// </summary>
    public decimal TotalPayments { get; init; }
    
    /// <summary>
    /// Сума, полученная по вкладам (
    /// </summary>
    public decimal TotalPayoffs { get; init; }

    /// <summary>
    /// Остаточная сумма всех кредитов - разница c прошлым месяцем в процентах
    /// </summary>
    public int? TotalDebtsDelta { get; init; } = null;

    /// <summary>
    /// Сумма, находящаяся во вкладах - разница c прошлым месяцем в процентах
    /// </summary>
    public int? TotalSavesDelta { get; init; } = null;

    /// <summary>
    /// Итоговая сумма выплат за всё время - разница c прошлым месяцем в процентах
    /// </summary>
    public int? TotalPaymentsDelta { get; init; } = null;

    /// <summary>
    /// Сума, полученная по вкладам - разница c прошлым месяцем в процентах
    /// </summary>
    public int? TotalPayoffsDelta { get; init; } = null;
}