using Application.Common.Models;
using Domain.Entities;
using MediatR;

namespace Application.Users.Queries.GetUserDetails;

public class GetStatisticListQuery : IRequest<IList<Statistic>?>
{
    public string UserEmail { get; set; }
}