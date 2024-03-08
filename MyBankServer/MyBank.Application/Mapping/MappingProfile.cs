using AutoMapper;
using MyBank.Core.Models;
using MyBank.DataAccess.Entities;

namespace MyBank.Application.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<UserEntity, User>();
        CreateMap<User, UserEntity>();
    }
}
