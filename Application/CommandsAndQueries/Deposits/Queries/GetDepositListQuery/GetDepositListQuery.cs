using Application.Common.Models;
using Domain.Entities;
using MediatR;

namespace Application.Users.Queries.GetUserDetails;

public class GetDepositListQuery : IRequest<IList<BankDeposit>?>
{
    public string UserEmail { get; set; }
}