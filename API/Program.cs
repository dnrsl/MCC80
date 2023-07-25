using API.Contracts;
using API.Data;
using API.Repositories;
using API.Services;
using API.Utilities.Handlers;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Connections;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var connection = builder.Configuration.GetConnectionString("DefaultConnection"); //didapatkan dari appsettings.json
// Add DbContext to the container. (mendaftarkan class DbContext)
builder.Services.AddDbContext<BookingDbContext>(option => option.UseSqlServer(connection));

// Add repositories to the container.
builder.Services.AddScoped<IUniversityRepository, UniversityRepository>();
builder.Services.AddScoped<IEducationRepository, EducationRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<IAccountRoleRepository, AccountRoleRepository>();
builder.Services.AddScoped<IRoomRepository, RoomRepository>();
builder.Services.AddScoped<IBookingRepository, BookingRepository>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();

// Add services to the container.
builder.Services.AddScoped<UniversityService>();
builder.Services.AddScoped<RoleService>();
builder.Services.AddScoped<RoomService>();
builder.Services.AddScoped<EmployeeService>();
builder.Services.AddScoped<AccountService>();
builder.Services.AddScoped<AccountRoleService>();
builder.Services.AddScoped<EducationService>();
builder.Services.AddScoped<BookingService>();
builder.Services.AddScoped<GenerateNikHandler>();

builder.Services.AddSingleton<GenerateNikHandler>();

// Register FluentValidation
builder.Services.AddFluentValidationAutoValidation()
       .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());


builder.Services.AddControllers();
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
