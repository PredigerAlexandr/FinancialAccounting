using Application.Common.Models;
using Domain.Entities;
using MediatR;

namespace Application.Users.Queries.GetUserDetails;

public class GetDebtListQuery : IRequest<IList<Debt>?>
{
    public string UserEmail { get; set; }
}