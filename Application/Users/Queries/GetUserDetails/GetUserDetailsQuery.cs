using Application.Common.Models;
using MediatR;

namespace Application.Users.Queries.GetUserDetails;

public class GetUserDetailsQuery:IRequest<UserDto>
{
    public int Id { get; set; }
}