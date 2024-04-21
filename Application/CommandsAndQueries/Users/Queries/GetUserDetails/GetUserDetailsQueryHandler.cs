using Application.Common.Exceptions;
using Application.Interfaces;
using Application.Users.Queries.GetUserDetails;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CommandsAndQueries.Users.Queries.GetUserDetails;

public class GetUserDetailsQueryHandler : IRequestHandler<GetUserDetailsQuery, User>
{
    private readonly IDbContext _dbContext; 
    private readonly IMapper _mapper;

    public GetUserDetailsQueryHandler(IDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }
    

    public async Task<User> Handle(GetUserDetailsQuery request, CancellationToken cancellationToken)
    {
        var entity = await _dbContext.Users.Where(u => u.Email == request.Email).FirstOrDefaultAsync(cancellationToken: cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(User), request.Email);
        }

        return entity;
    }
}