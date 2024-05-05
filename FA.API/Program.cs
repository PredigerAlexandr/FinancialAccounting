using System.Net.NetworkInformation;
using System.Reflection;
using System.Text;
using Application.Common.Mappings;
using Application.Interfaces;
using Application.Services;
using Application.Services.DebtService;
using Application.Services.IdentityService;
using Application.Services.MoneySpendingService;
using Application.Services.StatisticService;
using Application.Services.UserService;
using Application.Users.Commands.CreateUser;
using AutoMapper;
using Domain.Interfaces;
using Domain.Models.JWT;
using FluentValidation.AspNetCore;
using Hangfire;
using Hangfire.PostgreSql;
using Infrastructure;
using Infrastructure.Jobs;
using Infrastructure.Jobs.JobService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            // укзывает, будет ли валидироваться издатель при валидации токена
            ValidateIssuer = true,
            // строка, представляющая издателя
            ValidIssuer = JwtOptions.ISSUER,
 
            // будет ли валидироваться потребитель токена
            ValidateAudience = true,
            // установка потребителя токена
            ValidAudience = JwtOptions.AUDIENCE,
            // будет ли валидироваться время существования
            ValidateLifetime = true,
 
            // установка ключа безопасности
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(JwtOptions.KEY)),
            // валидация ключа безопасности
            ValidateIssuerSigningKey = true,
        };
    });
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<IDbContext, PostgreSqlDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgreSQLConnection")));
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
builder.Services.AddAutoMapper(typeof(AppMappingProfile));

foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
{
    builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(assembly));
}

builder.Services.AddControllers()
    .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<CreateUserCommand>());

builder.Services.AddScoped<IIdentityService,IdentityService>();
builder.Services.AddScoped<IUserService,UserService>();
builder.Services.AddScoped<IStatisticService,StatisticService>();
builder.Services.AddScoped<IDebtService,DebtService>();
builder.Services.AddScoped<IMoneySpendingService,MoneySpendingService>();
builder.Services.AddScoped<IVacancyService, VacancyService>();
builder.Services.AddScoped<IPaymentsService, PaymentsService>();

builder.Services.AddHangfire(configuration => configuration
    .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
    .UseSimpleAssemblyNameTypeSerializer()
    .UseRecommendedSerializerSettings()
    .UsePostgreSqlStorage("Host=localhost;Port=5432;Database=FA;Username=postgres;Password=0000"));
builder.Services.AddHangfireServer(options => { options.Queues = new[] { "default" }; });

builder.Services.AddHostedService<VacancySearchJob>();
builder.Services.AddHostedService<PaymentsCheckJob>();


builder.Services.AddCors(options => options.AddPolicy("AllowAll", policy =>
{
    policy.AllowAnyOrigin();
    policy.AllowAnyHeader();
    policy.AllowAnyMethod();
}));
builder.Services.AddControllers();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseHttpsRedirection();
app.UseCors("AllowAll");

app.UseEndpoints(endpoints => endpoints.MapControllers());

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}