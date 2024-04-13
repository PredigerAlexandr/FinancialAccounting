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

        await _dbContext.BankDeposits.AddAsync(new BankDeposit()
        {
            Name = request.Name,
            FullSum = request.FullSum,
            Rate = request.Rate,
            Capitalization = request.Capitalization,
            DateStart = request.DateStart,
            MonthsTotal = request.MonthsTotal,
            User = user
        }, cancellationToken);

        await _dbContext.SaveChangesAsync(cancellationToken);


        return 1;
    }
}