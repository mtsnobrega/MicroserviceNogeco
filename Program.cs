using MicroserviceNogeco.Controllers;
using MicroserviceNogeco.Models;
using MicroserviceNogeco.Models.StrategyNotificationSender;
using MicroserviceNogeco.Models.StrategySearchFrellancer;
using MicroserviceNogeco.Repository;
using MicroserviceNogeco.Service;
using Microsoft.Extensions.Http;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<NotificationService>();

builder.Services.AddScoped<IFrellaRepository, FrellaRepository>();

builder.Services.AddHttpClient<INotificationSender, WhatsAppSender>();


builder.Services.AddScoped<IFrellancerSearchStrategy, NormalSearchStrategy>();
builder.Services.AddScoped<NormalSearchStrategy>(); // Opcional, mas útil para injeção direta
builder.Services.AddScoped<EmergencySearchStrategy>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Definindo as rotas para os endpoints da api 
app.MapRoutesNotification();
app.MapRoutesPresence();

app.UseAuthorization();

app.MapControllers();

app.Run();