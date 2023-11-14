using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using SneakersCollection.Api.Extensions;
using SneakersCollection.Api.Models;
using SneakersCollection.Application.Services;
using SneakersCollection.Data.Contexts;
using SneakersCollection.Data.Repositories;
using SneakersCollection.Domain.Interfaces.Repositories;
using SneakersCollection.Domain.Interfaces.Services;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddAuthSettings(builder.Configuration);

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

builder.Services.AddDbContext<SneakersCollectionContext>(options =>
            options.UseInMemoryDatabase(databaseName: "InMemorySneakersCollectionDb"));

builder.Services.AddScoped<ISneakerRepository, SneakerRepository>();
builder.Services.AddScoped<IBrandRepository, BrandRepository>();
builder.Services.AddScoped<ISneakerService, SneakerService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.AddSwaggerUISettings(builder.Configuration);
}

app.UseAuthentication();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<SneakersCollectionContext>();

    // Ensure the database is created
    context.Database.EnsureCreated();
}

app.Run();
