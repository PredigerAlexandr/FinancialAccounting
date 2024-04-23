using Application.CommandsAndQueries.Debts.Commands.CreateDebt;
using Application.Common.Constants;
using Application.Common.Exceptions;
using Application.Interfaces;
using Application.Services.IdentityService;
using AutoMapper;
using Domain.Entities;
using Domain.Models.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace Application.CommandsAndQueries.MoneySpendings.Commands.CreateMoneySpending;

public class CreateMoneySpendingCommandHandler : IRequestHandler<CreateMoneySpendingCommand, int>
{
    private readonly IDbContext _dbContext;
    private readonly IIdentityService _identityService;
    private readonly IMapper _mapper;

    public CreateMoneySpendingCommandHandler(IDbContext dbContext, IIdentityService identityService, IMapper mapper)
    {
        _dbContext = dbContext;
        _identityService = identityService;
        _mapper = mapper;
    }


    public async Task<int> Handle(CreateMoneySpendingCommand request, CancellationToken cancellationToken)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == request.UserEmail,
            cancellationToken: cancellationToken);
        if (user == null)
            throw new NotFoundException(nameof(User), request.UserEmail);
        if(!Constant.TypeMoneySpendings.Contains(request.Type)&&!Constant.TypeMoneySpendingsForAuto.Contains(request.Type))
            throw new NotFoundException(nameof(request.Type), request.Type);

        await _dbContext.MoneySpendings.AddAsync(new MoneySpending
        {
            Type = request.Type,
            Amount = request.Amount,
            Date = request.Date,
            User = user
        }, cancellationToken);

        await _dbContext.SaveChangesAsync(cancellationToken);


        return 1;
    }
}