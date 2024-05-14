using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyBank.Persistence.Interfaces;
using MyBank.Persistence.Repositories;

namespace MyBank.Persistence.Extensions;

public static class RepositoriesExtension
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUsersRepository, UsersRepository>();
        services.AddScoped<IAdminRepository, AdminRepository>();
        services.AddScoped<ICardPackagesRepository, CardPackagesRepository>();
        services.AddScoped<ICardsRepository, CardsRepository>();
        services.AddScoped<ICreditAccountsRepository, CreditAccountsRepository>();
        services.AddScoped<ICreditPackagesRepository, CreditPackagesRepository>();
        services.AddScoped<ICreditPaymentsRepository, CreditPaymentsRepository>();
        services.AddScoped<ICreditRequestsRepository, CreditRequestsRepository>();
        services.AddScoped<ICurrenciesRepository, CurrenciesRepository>();
        services.AddScoped<IDepositAccountsRepository, DepositAccountsRepository>();
        services.AddScoped<IDepositAccrualsRepository, DepositAccrualsRepository>();
        services.AddScoped<IDepositPackagesRepository, DepositPackagesRepository>();
        services.AddScoped<IMessagesRepository, MessagesRepository>();
        services.AddScoped<IModeratorsRepository, ModeratorsRepository>();
        services.AddScoped<IPersonalAccountsRepository, PersonalAccountsRepository>();
        services.AddScoped<ITransactionsRepository, TransactionsRepository>();
        services.AddScoped<IUsersRepository, UsersRepository>();

        return services;
    }
}
