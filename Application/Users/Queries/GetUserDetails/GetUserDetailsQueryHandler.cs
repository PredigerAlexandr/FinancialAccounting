using Application.Common.Exceptions;
using Application.Common.Models;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Users.Queries.GetUserDetails;

public class GetUserDetailsQueryHandler : IRequestHandler<GetUserDetailsQuery, UserDto>
{
    private readonly IDbContext _dbContext; 
    private readonly IMapper _mapper;

    public GetUserDetailsQueryHandler(IDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }
    

    public async Task<UserDto> Handle(GetUserDetailsQuery request, CancellationToken cancellationToken)
    {
        var entity = await _dbContext.Users.Where(u => u.Id == request.Id).FirstOrDefaultAsync(cancellationToken: cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(User), request.Id);
        }

        return _mapper.Map<UserDto>(entity);
    }
}