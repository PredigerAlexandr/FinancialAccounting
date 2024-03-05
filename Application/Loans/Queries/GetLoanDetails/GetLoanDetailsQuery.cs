using Application.Common.Models;
using Domain.Entities;
using MediatR;

namespace Application.Users.Queries.GetUserDetails;

public class GetLoanDetailsQuery : IRequest<Loan>
{
    public Guid Id { get; set; }
}