﻿namespace MyBank.Persistence.Mapping;

public class MappingProfileDatabase : Profile
{
    public MappingProfileDatabase()
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
    }
}
