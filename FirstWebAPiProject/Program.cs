using FirstClassLibrary;
using FirstClassLibrary.Entity;
using FirstWebAPiProject.Logger;
using FirstWebAPiProject.Model.Dto;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<DataContext>(option =>
option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//FluentValidation
builder.Services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<CourseDto>());


//Creating Logger using Depency Injection

builder.Services.AddScoped<IMyLogger, MyLogger>();
builder.Services.AddScoped<MyLogger>();


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
