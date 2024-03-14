using Domain.Entities;
using MediatR;

namespace Application.Loans.Queries.GetLoanDetails;

public class GetLoanDetailsQuery : IRequest<Loan>
{
    public string Name { get; set; }
    public string EmailUser { get; set; }
}