using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using Application.Common.Models;
using Application.Interfaces;
using Application.Services.IdentityService;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Users.Commands.CreateUser;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, UserDto>
{
    private readonly IDbContext _dbContext;
    private readonly IIdentityService _identityService;
    private readonly IMapper _mapper;

    public CreateUserCommandHandler(IDbContext dbContext, IIdentityService identityService, IMapper mapper)
    {
        _dbContext = dbContext;
        _identityService = identityService;
        _mapper = mapper;
    }


    public async Task<UserDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {


        var hashPassword = _identityService.GetMD5Hash(request.Password + request.Salt);
        var user = new User
        {
            Name = request.Name,
            MiddleName = request.MiddleName,
            Surname = request.Surname,
            Age = request.Age,
            Salary = request.Salary,
            Email = request.Email,
            Password = hashPassword
        };

        await _dbContext.Users.AddAsync(user, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return _mapper.Map<User,UserDto>(user);
    }
}