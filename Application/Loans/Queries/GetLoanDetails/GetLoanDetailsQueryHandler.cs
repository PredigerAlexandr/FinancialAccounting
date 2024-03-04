using Application.Common.Exceptions;
using Application.Common.Models;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Users.Queries.GetUserDetails;

public class GetLoanDetailsQueryHandler : IRequestHandler<GetLoanDetailsQuery, Loan>
{
    private readonly IDbContext _dbContext; 
    private readonly IMapper _mapper;

    public GetLoanDetailsQueryHandler(IDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }
    

    public async Task<Loan> Handle(GetLoanDetailsQuery request, CancellationToken cancellationToken)
    {
        var entity = await _dbContext.Loans.Where(u => u.Id == request.Id).FirstOrDefaultAsync(cancellationToken: cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(User), request.Id);
        }

        return entity;
    }
}