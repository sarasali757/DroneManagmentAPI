using DroneManagmentAPI.Models;
using DroneManagmentAPI.Models.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddHostedService<CronJob>();

builder.Services.AddDbContext<DroneContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("Connection")));

builder.Services.AddScoped<DroneRepository>();
builder.Services.AddScoped<MedicationRepository>();
builder.Services.AddScoped<DispatchRepository>();
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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
