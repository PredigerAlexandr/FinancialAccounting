using Application.Common.Models;
using Domain.Entities;
using MediatR;

namespace Application.Loans.Commands.CreateLoan;

public class CreateLoanCommand : IRequest<int>
{
    public string UserEmail { get; set; }
    public decimal FullSum { get; set; }
    public decimal CurrentSum { get; set; }
    public string Name { get; set; }
    public double Rate { get; set; }
    public string Type { get; set; }
}