using Application.Common.Models;
using Domain.Entities;
using MediatR;

namespace Application.Users.Queries.GetUserDetails;

public class GetLoanListDetailsQuery : IRequest<IList<Loan>>
{
    public string UserEmail { get; set; }
}