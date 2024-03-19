using Microsoft.Extensions.DependencyInjection;
using MyBank.Application.Interfaces;
using MyBank.Application.Services;
using MyBank.Persistence.Interfaces;
using MyBank.Persistence.Repositories;

namespace MyBank.Application.Extensions;

public static class ServicesExtension
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IAdminService, AdminService>();
        services.AddScoped<ICardPackagesService, CardPackagesService>();
        services.AddScoped<ICardsService, CardsService>();
        services.AddScoped<ICreditAccountsService, CreditAccountsService>();
        services.AddScoped<ICreditPaymentsService, CreditPaymentsService>();
        services.AddScoped<ICreditRequestsService, CreditRequestsService>();
        services.AddScoped<ICurrencyService, CurrencyService>();
        services.AddScoped<IDepositAccountsService, DepositAccountsService>();
        services.AddScoped<IDepositAccrualsService, DepositAccrualsService>();
        services.AddScoped<IMessagesService, MessagesService>();
        services.AddScoped<IModeratorsService, ModeratorsService>();
        services.AddScoped<IPersonalAccountsService, PersonalAccountsService>();
        services.AddScoped<ITransactionsService, TransactionsService>();
        services.AddScoped<IUserService, UserService>();

        return services;
    }
}
