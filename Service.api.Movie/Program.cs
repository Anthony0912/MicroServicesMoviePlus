using FluentValidation;
using FluentValidation.AspNetCore;
using Service.api.Movie.DBConfig;
using Service.api.Movie.Entities;
using Service.api.Movie.Repository;
using Service.api.Movie.Validator;

var builder = WebApplication.CreateBuilder(args);

//Connection Data Base SQLServer
DBConnection connectionDb = new DBConnection(builder);
connectionDb.Connect();

// Add services to the container.
builder.Services.AddControllers();

// Add Fluent Validator
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddScoped<IValidator<EMovie>, MovieValidator>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Add Transient
builder.Services.AddTransient<MovieRepository>();

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
