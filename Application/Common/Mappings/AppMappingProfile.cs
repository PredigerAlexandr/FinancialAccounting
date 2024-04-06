using Application.Common.Models;
using AutoMapper;
using Domain.Entities;

namespace Application.Common.Mappings;

public class AppMappingProfile:Profile
{
    public AppMappingProfile()
    {
        CreateMap<User, UserDto>();
        CreateMap<Debt, DebtDto>();
    }
}