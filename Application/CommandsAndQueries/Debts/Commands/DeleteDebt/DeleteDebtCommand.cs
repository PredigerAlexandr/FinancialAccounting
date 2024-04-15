using MediatR;

namespace Application.Debtss.Commands.DeleteUser;

public class DeleteDebtCommand : IRequest<int>
{
    public string Name { get; set; }
    public string Email { get; set; }
}