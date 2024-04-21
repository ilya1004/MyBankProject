using MyBank.API.DataTransferObjects.CardDto;
using MyBank.API.DataTransferObjects.CardPackageDtos;
using MyBank.API.DataTransferObjects.CreditAccountDtos;
using MyBank.API.DataTransferObjects.CreditPaymentDtos;
using MyBank.API.DataTransferObjects.CurrencyDtos;
using MyBank.API.DataTransferObjects.DepositAccountDtos;
using MyBank.API.DataTransferObjects.MessageDtos;
using MyBank.API.DataTransferObjects.PersonalAccountDtos;
using MyBank.API.DataTransferObjects.TransactionDtos;
using MyBank.API.DataTransferObjects.UserDtos;

namespace MyBank.API.Mapping;

public class MappingProfileDtos : Profile
{
    public MappingProfileDtos()
    {
        CreateMap<CardDto, Card>();
        CreateMap<CardPackageDto, CardPackage>();
        CreateMap<CreditAccountDto, CreditAccount>();
        CreateMap<CreditPaymentDto, CreditPayment>();
        CreateMap<CurrencyDto, Currency>();
        CreateMap<DepositAccountDto, DepositAccount>();
        CreateMap<MessageDto, Message>();
        CreateMap<PersonalAccountDto, PersonalAccount>();
        CreateMap<TransactionDto, Transaction>();
        CreateMap<UserDto, User>();
        CreateMap<RegisterUserDto, User>();
    }
}
