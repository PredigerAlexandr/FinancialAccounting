using Application.Common.Models;
using Domain.Entities;
using MediatR;

namespace Application.Users.Commands.CreateUser;

public class CreateLoanCommand : IRequest<int>
{
    public string UserEmail { get; set; }
    public decimal Sum { get; set; }
    public string Name { get; set; }
    public double Procent { get; set; }
    public string Type { get; set; }
}