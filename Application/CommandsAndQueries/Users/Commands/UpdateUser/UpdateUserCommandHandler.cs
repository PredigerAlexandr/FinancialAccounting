using Application.Common.Exceptions;
using Application.Interfaces;
using Application.Users.Commands.AddUser;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CommandsAndQueries.Users.Commands.UpdateUser;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, short>
{
    private readonly IDbContext _dbContext;
    public UpdateUserCommandHandler(IDbContext dbContext) => _dbContext = dbContext;

    public async Task<short> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var entity = await _dbContext.Users.FirstOrDefaultAsync(user =>
            user.Email == request.Email, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(User), request.Email);
        }

        entity.Name = request.Name;
        entity.MiddleName = request.MiddleName;
        entity.Surname = request.Surname;
        entity.Age = request.Age;
        entity.Salary = request.Salary;
        entity.City = request.City;
        entity.ProfileWork = request.ProfileWork;
        entity.JkhSummer = request.JkhSummer;
        entity.JkhWinter = request.JkhWinter;
        entity.IsAuto = request.IsAuto;
        entity.AnotherPayments = request.AnotherPayments;

        await _dbContext.SaveChangesAsync(cancellationToken);

        return 1;
    }
}