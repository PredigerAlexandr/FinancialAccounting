using Application.Common.Models;
using Domain.Entities;
using Domain.Interfaces;
using System.Linq;
using Domain.Models.DTO;
using MediatR;

namespace Application.Services.DebtService;

public class DebtService : IDebtService
{
    private readonly IMediator _mediator;

    public DebtService(IMediator mediator)
    {
        _mediator = mediator;
    }

    public List<DebtDto> OfferTransferFromDepositToDebt(List<DebtDto> debts, IList<BankDeposit>? deposits)
    {
        foreach (var debt in debts)
        {
            //находим сколько месяцев осталось платить кредит
            var months = debt.MonthsTotal - GetDifMonths(debt.DateStart, DateTime.Now);
            //находим сумму переплаты по кредиту
            var overpaymentWithoutDeposit = CalculateOverpaymentSum(debt.CurrentSum, debt.Rate, months);
            
            foreach (var deposit in deposits)
            {
                //находим переплату с учётом погашения кредита засчёт вклада
                var overpaymentWitDeposit = CalculateOverpaymentSum(debt.CurrentSum-deposit.FullSum, debt.Rate, months);
                //находим сумму выплат по вкладу за период, равный остатку периода кредита или концу вклада.
                var profitDeposit = GetProfitDeposit(deposit, months);
                //находим дельту переплаты по кредиту с погашениями из вкладов и без 
                var deltaOverpayment = overpaymentWithoutDeposit - overpaymentWitDeposit;

                if (deltaOverpayment > profitDeposit)
                {
                    debt.OffersDepositToDebt.Add(new 
                    {
                        Name = deposit.Name,
                        Value=deltaOverpayment
                    });
                }
            }
        }

        return debts;
    }

    public decimal CalculateOverpaymentSum(decimal sum, decimal? rate, int totalMonths)
    {
        rate ??= 0;
        
        decimal koef = (decimal)(rate / 1200 * (decimal)Math.Pow((double)(1 + (rate / 1200)), totalMonths)
                                 / (decimal)(Math.Pow((double)(1 + rate/1200), totalMonths) - 1));
        
        var overpaymentSum = (sum * koef * totalMonths) - sum;

        return overpaymentSum;
    }

    public decimal GetProfitDeposit(BankDeposit deposit, int creditMonths)
    {
        var calculateMonths = (GetDifMonths(deposit.DateStart, DateTime.Now)-deposit.MonthsTotal) >= creditMonths
            ? creditMonths
            : deposit.MonthsTotal - GetDifMonths(deposit.DateStart, DateTime.Now);
        
        decimal resultSum;
        if (deposit.Capitalization == true)
        {
            resultSum = (deposit.FullSum * (decimal)Math.Pow((1 + deposit.Rate / 1200),calculateMonths))-deposit.FullSum;
        }
        else
        {
            resultSum = deposit.FullSum + (deposit.FullSum * (decimal)(deposit.Rate / 1200) * calculateMonths);
        }

        return resultSum;
    }
    

    public int GetDifMonths(DateTime startDate, DateTime endDate)
    {
        var difMonths =
            System.Globalization.DateTimeFormatInfo.InvariantInfo.Calendar.GetMonthsInYear(startDate.Year) *
            (endDate.Year - startDate.Year) + (endDate.Month - startDate.Month);
        return difMonths;
    }
}