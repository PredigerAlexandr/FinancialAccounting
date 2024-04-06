using Application.Common.Exceptions;
using Application.Debtss.Commands.CreateDebts;
using Application.Interfaces;
using Application.Services.IdentityService;
using AutoMapper;
using Domain.Entities;
using Domain.Models.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Deposits.Commands.CreateDeposit;

public class CreatePayoffCommandHandler : IRequestHandler<CreateDebtsCommand, int>
{
    private readonly IDbContext _dbContext;
    private readonly IIdentityService _identityService;
    private readonly IMapper _mapper;

    public CreatePayoffCommandHandler(IDbContext dbContext, IIdentityService identityService, IMapper mapper)
    {
        _dbContext = dbContext;
        _identityService = identityService;
        _mapper = mapper;
    }


    public async Task<int> Handle(CreateDebtsCommand request, CancellationToken cancellationToken)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == request.UserEmail,
            cancellationToken: cancellationToken);
        if (user == null)
            throw new NotFoundException(nameof(User), request.UserEmail);

        _dbContext.Debts.AddAsync(new Debt
        {
            Name = request.Name,
            FullSum = request.FullSum,
            CurrentSum = request.CurrentSum,
            Rate = (decimal)request.Rate,
            Type = (DebtType)Enum.Parse(typeof(DebtType), request.Type),
            User = user
        }, cancellationToken);

        await _dbContext.SaveChangesAsync(cancellationToken);


        return 1;
    }
}