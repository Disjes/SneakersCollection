using Microsoft.EntityFrameworkCore;
using SneakersCollection.Data.Contexts;
using SneakersCollection.Data.Repositories;
using SneakersCollection.Domain.Interfaces.Repositories;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

builder.Services.AddDbContext<SneakersCollectionContext>(options =>
            options.UseInMemoryDatabase(databaseName: "InMemorySneakersCollectionDb"));

builder.Services.AddScoped<ISneakerRepository, SneakerRepository>();

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
