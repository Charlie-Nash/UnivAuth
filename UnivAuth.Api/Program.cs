using UnivAuth.Api.Helpers;
using UnivAuth.Application.UseCases;
using UnivAuth.Domain.Interfaces;
using UnivAuth.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<LoginService>();
builder.Services.AddScoped<AppAuth>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapGet("/", () => "UnivAuth: En línea");
app.MapControllers();

app.Run();