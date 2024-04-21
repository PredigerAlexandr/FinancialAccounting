using Application.CommandsAndQueries.Deposits.Commands.CreateDeposit;
using Application.Common.Models;
using AutoMapper;
using Domain.Entities;
using Domain.Models.DTO;

namespace Application.Common.Mappings;

public class AppMappingProfile:Profile
{
    public AppMappingProfile()
    {
        CreateMap<User, UserDto>();
        CreateMap<User, UserInfoDto>();
        CreateMap<Debt, DebtDto>();
        CreateMap<BankDeposit, DepositDto>();
        CreateMap<CreateDepositCommand, BankDeposit>();
    }
}