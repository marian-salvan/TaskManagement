using Microsoft.EntityFrameworkCore;
using TaskManagement.API.Extensions;
using TaskManagement.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddRepositories();
builder.Services.AddServices();

//using an in-memory database for testing
//TODO use a real database for production
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseInMemoryDatabase("TaskManagement"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//TODO: configure application security (authentication/authorization) for production
app.UseAuthorization();

app.MapControllers();

app.UseGlboalExecption();

app.Run();
