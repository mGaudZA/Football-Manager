using Football_Manager.Interfaces;
using Football_Manager.Models.Tables;
using Football_Manager.Providers;
using  Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContextPool<FootballManagerContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("FootBallManagerDatabase")));
builder.Services.AddTransient<IPlayerProvider,PlayerProvider>();
builder.Services.AddSingleton<ICustomLogger, ConsoleLoggingProvider>();
builder.Services.AddTransient<ITeamProvider, TeamProvider>();
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
