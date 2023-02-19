using CodeChallenge_SV.BusinessLogicLayer;
using CodeChallenge_SV.Data;
using CodeChallenge_SV.DataAccessLayer;
using CodeChallenge_SV.DataAccessLayerInteraces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
const string allowedOriginSetting = "AllowedOrigin";
// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<SearchContext>(opt =>
    opt.UseInMemoryDatabase("Search"));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

IServiceCollection serviceCollection = builder.Services.AddTransient<ISearchDal, SearchDal>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors(corsBuilder => {
        corsBuilder.WithOrigins(builder.Configuration[allowedOriginSetting])
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
    app.Seed();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
