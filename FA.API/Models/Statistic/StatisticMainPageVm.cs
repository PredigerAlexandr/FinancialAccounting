namespace CreditAPI.Models.Statistic;

public class StatisticMainPageVm
{
    /// <summary>
    /// Остаточная сумма всех кредитов
    /// </summary>
    public decimal TotalDebts { get; set; }
    
    /// <summary>
    /// Сумма, находящаяся во вкладах
    /// </summary>
    public decimal TotalDeposits{ get; set; }
    
    /// <summary>
    /// Итоговая сумма выплат за всё время
    /// </summary>
    public decimal TotalPayments { get; set; }
    
    /// <summary>
    /// Сума, полученная по вкладам (
    /// </summary>
    public decimal TotalPayoffs { get; set; }

    /// <summary>
    /// Остаточная сумма всех кредитов - разница c прошлым месяцем в процентах
    /// </summary>
    public int? TotalDebtsDelta { get; set; } = null;

    /// <summary>
    /// Сумма, находящаяся во вкладах - разница c прошлым месяцем в процентах
    /// </summary>
    public int? TotalDepositsDelta { get; set; } = null;

    /// <summary>
    /// Итоговая сумма выплат за всё время - разница c прошлым месяцем в процентах
    /// </summary>
    public int? TotalPaymentsDelta { get; set; } = null;

    /// <summary>
    /// Сума, полученная по вкладам - разница c прошлым месяцем в процентах
    /// </summary>
    public int? TotalPayoffsDelta { get; set; } = null;
}