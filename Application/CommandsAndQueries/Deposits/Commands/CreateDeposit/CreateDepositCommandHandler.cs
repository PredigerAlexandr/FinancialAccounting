using System.Runtime.InteropServices.JavaScript;
using Application.Common.Exceptions;
using Application.Debtss.Commands.CreateDebts;
using Application.Interfaces;
using Application.Services.IdentityService;
using AutoMapper;
using Domain.Entities;
using Domain.Models.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CommandsAndQueries.Deposits.Commands.CreateDeposit;

public class CreateDepositCommandHandler : IRequestHandler<CreateDepositCommand, int>
{
    private readonly IDbContext _dbContext;
    private readonly IIdentityService _identityService;
    private readonly IMapper _mapper;

    public CreateDepositCommandHandler(IDbContext dbContext, IIdentityService identityService, IMapper mapper)
    {
        _dbContext = dbContext;
        _identityService = identityService;
        _mapper = mapper;
    }


    public async Task<int> Handle(CreateDepositCommand request, CancellationToken cancellationToken)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == request.UserEmail,
            cancellationToken: cancellationToken);
        if (user == null)
            throw new NotFoundException(nameof(User), request.UserEmail);

        var deposit = new BankDeposit()
        {
            Name = request.Name,
            FullSum = request.FullSum,
            Rate = request.Rate,
            Capitalization = request.Capitalization,
            DateStart = request.DateStart,
            MonthsTotal = request.MonthsTotal,
            User = user
        };

        var currentDate = DateTime.Now;
        var startDate = deposit.DateStart;
        while (startDate.Month != currentDate.Month || startDate.Year != currentDate.Year)
        {
            var payoff = new Payoff()
            {
                Amount = Decimal.Round(deposit.FullSum * (decimal)deposit.Rate / 1200, 1),
                DateCreate = startDate,
                BankDeposit = deposit
            };
            if (deposit.Capitalization)
            {
                deposit.FullSum += payoff.Amount;
            }
            deposit.Payoffs.Add(payoff);
            startDate = startDate.AddMonths(1);
        }

        await _dbContext.BankDeposits.AddAsync(deposit, cancellationToken);

        await _dbContext.SaveChangesAsync(cancellationToken);


        return 1;
    }
}