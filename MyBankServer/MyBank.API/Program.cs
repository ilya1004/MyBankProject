using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using MyBank.Application.Interfaces.Services;
using MyBank.Application.Interfaces.Utils;
using MyBank.Application.Services;
using MyBank.Application.Utils;
using MyBank.DataAccess;
using MyBank.DataAccess.Enterfaces;
using MyBank.DataAccess.Repositories;
using MyBank.DataAccess.Mapping;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddAutoMapper(typeof(Program).Assembly);

builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection(nameof(JwtOptions)));

builder.Services.AddDbContext<MyBankDbContext>(
	options => options.UseNpgsql(builder.Configuration.GetConnectionString(nameof(MyBankDbContext))));

builder.Services.AddSwaggerGen(c => c.SwaggerDoc("v1", new OpenApiInfo { Title = "MyBank API", Version = "v1" }));

builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<IJwtProvider, JwtProvider>();
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
//builder.Services.AddScoped<PasswordHasher>();

builder.Services.AddScoped<IUserRepository, UserRepository>();


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
//app.UseStaticFiles();

app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
	"Default", 
	"{controller=Value}/{Action=Index}");


//app.MapGet("/secret", (ClaimsPrincipal user) => $"Hello {user.Identity?.Name}. My secret");


app.Run();
