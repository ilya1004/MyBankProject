using AutoMapper;
using MyBank.Core.Models;
using MyBank.DataAccess.Entities;

namespace MyBank.DataAccess.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<UserEntity, User>();

        CreateMap<User, UserEntity>()
            .ForMember(dest => dest.PersonalAccounts,
            src => src.MapFrom(x => x.PersonalAccounts))
            .ForMember(dest => dest.CreditAccounts,
            src => src.MapFrom(x => x.CreditAccounts))
            .ForMember(dest => dest.DepositAccounts,
            src => src.MapFrom(x => x.DepositAccounts))
            .ForMember(dest => dest.Cards,
            src => src.MapFrom(x => x.Cards))
            .ForMember(dest => dest.CreditRequests,
            src => src.MapFrom(x => x.CreditRequests))
            .ForMember(dest => dest.CreditPayments,
            src => src.MapFrom(x => x.CreditPayments))
            .ForMember(dest => dest.Messages,
            src => src.MapFrom(x => x.Messages));

        //CreateMap<>
    }
}

//public List<PersonalAccountEntity> PersonalAccounts { get; set; } = [];
//public List<CreditAccountEntity> CreditAccounts { get; set; } = [];
//public List<DepositAccountEntity> DepositAccounts { get; set; } = [];
//public List<CardEntity> Cards { get; set; } = [];
//public List<CreditRequestEntity> CreditRequests { get; set; } = [];
//public List<CreditPaymentEntity> CreditPayments { get; set; } = [];
//public List<MessageEntity> Messages { get; set; } = [];