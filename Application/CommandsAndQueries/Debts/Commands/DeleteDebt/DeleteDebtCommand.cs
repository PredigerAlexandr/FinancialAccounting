using MediatR;

namespace Application.Debtss.Commands.DeleteUser;

public class DeleteDebtsCommand : IRequest<int>
{
    public string Name { get; set; }
    public string Email { get; set; }
}