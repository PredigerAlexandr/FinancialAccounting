using Application.Common.Models;
using Domain.Entities;
using MediatR;

namespace Application.Debtss.Commands.CreateDebts;

public class CreateStatisticCommand : IRequest<int>
{
    public string UserEmail { get; set; }
    public decimal FullSum { get; set; }
    public decimal CurrentSum { get; set; }
    public string Name { get; set; }
    public double Rate { get; set; }
    public string Type { get; set; }
}