using CodeChallenge_SV.BusinessLogicLayer;
using CodeChallenge_SV.DataAccessLayer;
using CodeChallenge_SV.DataAccessLayerInteraces;

var builder = WebApplication.CreateBuilder(args);
const string allowedOriginSetting = "AllowedOrigin";
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
