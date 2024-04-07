using Domain.Entities;
using MediatR;

namespace Application.CommandsAndQueries.Statistics.Queries.GetStatisticDetails;

public class GetLastStatisticDetailsQuery : IRequest<Statistic?>
{
    public string EmailUser { get; set; }
}