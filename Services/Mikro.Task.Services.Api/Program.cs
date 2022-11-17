using Mikro.Task.Services.Db;
using Microsoft.EntityFrameworkCore;
using MikroTask.Services.Api.HostedServices;
using Microsoft.Extensions.DependencyInjection;
using Mikro.Task.Services.Application.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient();

builder.Services.AddDbContext<MovieDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), configure =>
    {
        configure.MigrationsAssembly("Mikro.Task.Services.Db");
    });
});

builder.Services.AddScoped<IHostedService, TheMovieDbService>();
builder.Services.AddScoped<ITheMovieService,TheMovieService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
