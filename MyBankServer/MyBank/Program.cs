using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using MyBank.API.Extensions;
using MyBank.Application.Interfaces;
using MyBank.Application.Services;
using MyBank.Application.Utils;
using MyBank.Database;
using MyBank.Database.Enterfaces;
using MyBank.Database.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddAutoMapper(typeof(Program).Assembly);

builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection(nameof(JwtOptions)));

builder.Services.AddDbContext<MyBankDbContext>(
    options => options.UseNpgsql(builder.Configuration.GetConnectionString(nameof(MyBankDbContext))));

builder.Services.AddSwaggerGen(c => c.SwaggerDoc("v1", new OpenApiInfo { Title = "MyBank API", Version = "v1" }));

builder.Services.AddApiAuthentication(builder.Configuration);
builder.Services.AddApiAuthorization();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAdminService, AdminService>();
builder.Services.AddScoped<ICardPackagesService, CardPackagesService>();
builder.Services.AddScoped<ICardsService, CardsService>();
builder.Services.AddScoped<ICreditAccountsService, CreditAccountsService>();
builder.Services.AddScoped<ICreditPaymentsService, CreditPaymentsService>();
builder.Services.AddScoped<ICreditRequestsService, CreditRequestsService>();
builder.Services.AddScoped<ICurrencyService, CurrencyService>();
builder.Services.AddScoped<IDepositAccountsService, DepositAccountsService>();
builder.Services.AddScoped<IDepositAccrualsService, DepositAccrualsService>();
builder.Services.AddScoped<IMessagesService, MessagesService>();
builder.Services.AddScoped<IModeratorsService, ModeratorsService>();
builder.Services.AddScoped<IPersonalAccountsService, PersonalAccountsService>();
builder.Services.AddScoped<ITransactionsService, TransactionsService>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<IJwtProvider, JwtProvider>();
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();

builder.Services.AddScoped<IUsersRepository, UsersRepository>();
builder.Services.AddScoped<IAdminRepository, AdminRepository>();
builder.Services.AddScoped<ICardPackagesRepository, CardPackagesRepository>();
builder.Services.AddScoped<ICardsRepository, CardsRepository>();
builder.Services.AddScoped<ICreditAccountsRepository, CreditAccountsRepository>();
builder.Services.AddScoped<ICreditPaymentsRepository, CreditPaymentsRepository>();
builder.Services.AddScoped<ICreditRequestsRepository, CreditRequestsRepository>();
builder.Services.AddScoped<ICurrenciesRepository, CurrenciesRepository>();
builder.Services.AddScoped<IDepositAccountsRepository, DepositAccountsRepository>();
builder.Services.AddScoped<IDepositAccrualsRepository, DepositAccrualsRepository>();
builder.Services.AddScoped<IMessagesRepository, MessagesRepository>();
builder.Services.AddScoped<IModeratorsRepository, ModeratorsRepository>();
builder.Services.AddScoped<IPersonalAccountsRepository, PersonalAccountsRepository>();
builder.Services.AddScoped<ITransactionsRepository, TransactionsRepository>();
builder.Services.AddScoped<IUsersRepository, UsersRepository>();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty;
    });
}
app.UseHttpsRedirection();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.UseCookiePolicy(new CookiePolicyOptions
{
    MinimumSameSitePolicy = SameSiteMode.Lax,
    HttpOnly = HttpOnlyPolicy.Always,
    Secure = CookieSecurePolicy.Always
});

app.MapControllerRoute("Default", "{controller=Value}/{Action=Index}");

app.Run();
