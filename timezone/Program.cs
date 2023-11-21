using Microsoft.EntityFrameworkCore;
using timezone.Api.Models;
using timezone.DataBase;
using timezone.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<TimeZoneConversion>();
builder.Services.AddScoped<OfferValidCheck>();
builder.Services.AddScoped<OfferResponseModel>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<TimeDBContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DBConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(o=> o.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());  

app.UseAuthorization();

app.MapControllers();

app.Run();
