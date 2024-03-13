using AutoMapper;
using MyBank.Core.Models;
using MyBank.Database.Entities;

namespace MyBank.Database.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Admin, AdminEntity>();
        CreateMap<AdminEntity, Admin>();

        CreateMap<Card, CardEntity>();
        CreateMap<CardEntity, Card>();

        CreateMap<CardPackage, CardPackageEntity>();
        CreateMap<CardPackageEntity, CardPackage>();

        CreateMap<CreditAccount, CreditAccountEntity>();
        CreateMap<CreditAccountEntity, CreditAccount>();

        CreateMap<CreditPayment, CreditPaymentEntity>();
        CreateMap<CreditPaymentEntity, CreditPayment>();

        CreateMap<CreditRequest, CreditRequestEntity>();
        CreateMap<CreditRequestEntity, CreditRequest>();

        CreateMap<Currency, CurrencyEntity>();
        CreateMap<CurrencyEntity, Currency>();

        CreateMap<DepositAccount, DepositAccountEntity>();
        CreateMap<DepositAccountEntity, DepositAccount>();

        CreateMap<DepositAccrual, DepositAccrualEntity>();
        CreateMap<DepositAccrualEntity, DepositAccrual>();

        CreateMap<Message, MessageEntity>();
        CreateMap<MessageEntity, Message>();

        CreateMap<Moderator, ModeratorEntity>();
        CreateMap<ModeratorEntity, Moderator>();

        CreateMap<PersonalAccount, PersonalAccountEntity>();
        CreateMap<PersonalAccountEntity, PersonalAccount>();

        CreateMap<Transaction, TransactionEntity>();
        CreateMap<TransactionEntity, Transaction>();

        CreateMap<UserEntity, User>();
        CreateMap<User, UserEntity>();

        //CreateMap<User, UserEntity>()
        //    .ForMember(dest => dest.PersonalAccounts,
        //    src => src.MapFrom(x => x.PersonalAccounts))
        //    .ForMember(dest => dest.CreditAccounts,
        //    src => src.MapFrom(x => x.CreditAccounts))
        //    .ForMember(dest => dest.DepositAccounts,
        //    src => src.MapFrom(x => x.DepositAccounts))
        //    .ForMember(dest => dest.Cards,
        //    src => src.MapFrom(x => x.Cards))
        //    .ForMember(dest => dest.CreditRequests,
        //    src => src.MapFrom(x => x.CreditRequests))
        //    .ForMember(dest => dest.CreditPayments,
        //    src => src.MapFrom(x => x.CreditPayments))
        //    .ForMember(dest => dest.Messages,
        //    src => src.MapFrom(x => x.Messages));
    }
}