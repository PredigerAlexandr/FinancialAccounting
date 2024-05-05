using Application.CommandsAndQueries.Debts.Commands.CreateDebt;
using Application.CommandsAndQueries.Payments.Commands.CreatePayment;
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

public class CreatePaymentCommandHandler : IRequestHandler<CreatePaymentCommand, int>
{
    private readonly IDbContext _dbContext;
    private readonly IIdentityService _identityService;
    private readonly IMapper _mapper;

    public CreatePaymentCommandHandler(IDbContext dbContext, IIdentityService identityService, IMapper mapper)
    {
        _dbContext = dbContext;
        _identityService = identityService;
        _mapper = mapper;
    }


    public async Task<int> Handle(CreatePaymentCommand request, CancellationToken cancellationToken)
    {
        var debt = await _dbContext.Debts.FirstOrDefaultAsync(d =>
            d.User.Email == request.UserEmail && d.Name == request.DebtName);
        if (debt == null)
            throw new NotFoundException(nameof(User), request.UserEmail);

        debt.Payments.Add(new Payment
        {
            DateCreate = DateTime.Now,
            Amount = request.Amount,
            Debt = debt
        });

        await _dbContext.SaveChangesAsync(cancellationToken);


        return 1;
    }
}