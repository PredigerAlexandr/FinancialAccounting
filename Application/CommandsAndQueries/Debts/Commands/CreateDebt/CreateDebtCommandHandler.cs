﻿using Application.Common.Exceptions;
using Application.Interfaces;
using Application.Services.IdentityService;
using AutoMapper;
using Domain.Entities;
using Domain.Models.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CommandsAndQueries.Debts.Commands.CreateDebt;

public class CreateDebtsCommandHandler : IRequestHandler<CreateDebtCommand, int>
{
    private readonly IDbContext _dbContext;
    private readonly IIdentityService _identityService;
    private readonly IMapper _mapper;

    public CreateDebtsCommandHandler(IDbContext dbContext, IIdentityService identityService, IMapper mapper)
    {
        _dbContext = dbContext;
        _identityService = identityService;
        _mapper = mapper;
    }


    public async Task<int> Handle(CreateDebtCommand request, CancellationToken cancellationToken)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == request.UserEmail,
            cancellationToken: cancellationToken);
        if (user == null)
            throw new NotFoundException(nameof(User), request.UserEmail);

        request.Type = request.Type is "кредит" or "Кредит" ? "Credit" : "Debt";

        await _dbContext.Debts.AddAsync(new Debt
        {
            Name = request.Name,
            FullSum = request.FullSum,
            CurrentSum = request.CurrentSum,
            Rate = (decimal)request.Rate,
            Type = (DebtType)Enum.Parse(typeof(DebtType), request.Type),
            DateStart = request.DateStart,
            MonthsTotal = request.MonthsTotal,
            User = user
        }, cancellationToken);

        await _dbContext.SaveChangesAsync(cancellationToken);
        return 1;
    }
}