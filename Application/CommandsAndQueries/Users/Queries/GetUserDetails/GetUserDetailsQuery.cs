using Domain.Entities;
using MediatR;

namespace Application.CommandsAndQueries.Users.Queries.GetUserDetails;

public class GetUserDetailsQuery : IRequest<User>
{
    public string Email { get; set; }
}