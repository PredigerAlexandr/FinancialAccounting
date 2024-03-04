using Application.Common.Exceptions;
using Application.Common.Models;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Users.Queries.GetUserDetails;

public class GetLoanListDetailsQueryHandler : IRequestHandler<GetLoanListDetailsQuery, IList<Loan>>
{
    private readonly IDbContext _dbContext; 
    private readonly IMapper _mapper;

    public GetLoanListDetailsQueryHandler(IDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }
    

    public async Task<IList<Loan>> Handle(GetLoanListDetailsQuery request, CancellationToken cancellationToken)
    {
        var entities = await _dbContext.Loans.Where(u => u.User.Email == request.UserEmail).ToListAsync(cancellationToken);

        if (entities == null)
        {
            throw new NotFoundException(nameof(User), request.UserEmail);
        }

        return entities;
    }
}