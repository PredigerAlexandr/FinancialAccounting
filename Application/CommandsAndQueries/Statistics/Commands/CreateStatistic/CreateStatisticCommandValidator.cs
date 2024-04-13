using Application.Debtss.Commands.CreateDebts;
using FluentValidation;

namespace Application.CommandsAndQueries.Statistics.Commands.CreateStatistic;

public class CreateStatisticCommandValidator:AbstractValidator<CreateStatisticCommand>
{
    public CreateStatisticCommandValidator()
    {
    }
}