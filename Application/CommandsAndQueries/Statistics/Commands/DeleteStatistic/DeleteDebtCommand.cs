using MediatR;

namespace Application.Debtss.Commands.DeleteUser;

public class DeleteStatisticCommand : IRequest<int>
{
    public string Name { get; set; }
    public string Email { get; set; }
}