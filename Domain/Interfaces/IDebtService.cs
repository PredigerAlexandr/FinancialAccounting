using Application.Common.Models;
using Domain.Entities;
using Domain.Models.DTO;

namespace Domain.Interfaces;

public interface IDebtService
{
    public List<DebtDto> OfferTransferFromDepositToDebt(List<DebtDto> debts, IList<BankDeposit>? deposits);
}