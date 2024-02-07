using System.Net.NetworkInformation;
using System.Reflection;
using System.Text;
using Application.Common.Mappings;
using Application.Interfaces;
using Application.Services;
using Application.Services.IdentityService;
using Application.Users.Commands.CreateUser;
using AutoMapper;
using FluentValidation.AspNetCore;
using Infrastructure;
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
            ValidIssuer = "http://localhost:5218/",
 
            // будет ли валидироваться потребитель токена
            ValidateAudience = true,
            // установка потребителя токена
            ValidAudience = "http://localhost:4200/",
            // будет ли валидироваться время существования
            ValidateLifetime = true,
 
            // установка ключа безопасности
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("mysupersecret_secretkey!123")),
            // валидация ключа безопасности
            ValidateIssuerSigningKey = true,
        };
    });
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<IDbContext, PostgreSqlDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgreSQLConnection")));
builder.Services.AddAutoMapper(typeof(AppMappingProfile));

foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
{
    builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(assembly));
}

builder.Services.AddControllers()
    .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<CreateUserCommand>());

builder.Services.AddSingleton<IIdentityService,IdentityService>();

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