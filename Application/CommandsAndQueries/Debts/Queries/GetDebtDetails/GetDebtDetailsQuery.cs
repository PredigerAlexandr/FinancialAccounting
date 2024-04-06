using Domain.Entities;
using MediatR;

namespace Application.Debtss.Queries.GetDebtsDetails;

public class GetDebtsDetailsQuery : IRequest<Debt>
{
    public string Name { get; set; }
    public string EmailUser { get; set; }
}