using System.Text.Json.Serialization;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using MyBank.API.Mapping;
using MyBank.Application.Extensions;
using MyBank.Application.Utils;
using MyBank.Persistence;
using MyBank.Persistence.Extensions;
using MyBank.Persistence.Mapping;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

services.AddControllers().AddJsonOptions(options => options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

services.AddAutoMapper(
    typeof(MappingProfileDatabase).Assembly,
    typeof(MappingProfileDtos).Assembly);

services.Configure<JwtOptions>(builder.Configuration.GetSection(nameof(JwtOptions)));

services.AddDbContext<MyBankDbContext>(options => 
    options.UseNpgsql(builder.Configuration.GetConnectionString(nameof(MyBankDbContext))));

services.AddSwaggerGen(c => c.SwaggerDoc("v1", new OpenApiInfo { Title = "MyBank API", Version = "v1" }));

services.AddApiAuthentication(builder.Configuration);
services.AddApiAuthorization();
services.AddApiUtils();

services.AddApplicationServices();
services.AddApplicationUtils();

services.AddRepositories();

var MyCorsPolicy = "myCorsPolicy";

services.AddCors(options =>
{
    options.AddPolicy(
        MyCorsPolicy,
        policyBuilder =>
        {
            policyBuilder.WithOrigins("http://localhost:3000");
            policyBuilder.AllowAnyMethod();
            policyBuilder.AllowAnyHeader();
            policyBuilder.AllowCredentials();
        }
    );
});

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

app.UseCors(MyCorsPolicy);

app.UseAuthentication();
app.UseAuthorization();

app.UseCookiePolicy(
    new CookiePolicyOptions
    {
        MinimumSameSitePolicy = SameSiteMode.Lax,
        HttpOnly = HttpOnlyPolicy.Always,
        Secure = CookieSecurePolicy.Always
    }
);

app.MapControllerRoute("Default", "{controller=Value}/{Action=Index}");

app.Run();
