using Application.Common.Exceptions;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Loans.Queries.GetLoanDetails;

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
        // var user = await _dbContext.Users.Where(u => u.Email == request.EmailUser)
        //     .FirstOrDefaultAsync(cancellationToken: cancellationToken);
        var entity = await _dbContext.Loans.Where(u => u.Name == request.Name)
            .Where(u => u.User.Email == request.EmailUser).FirstOrDefaultAsync(cancellationToken: cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(User), request.Name);
        }

        return entity;
    }
}