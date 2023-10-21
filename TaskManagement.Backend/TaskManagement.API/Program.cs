using FluentValidation;
using Microsoft.EntityFrameworkCore;
using TaskManagement.API.Extensions;
using TaskManagement.API.Helpers;
using TaskManagement.API.Profiles;
using TaskManagement.API.Validators;
using TaskManagement.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddRepositories();
builder.Services.AddExternalServices();

builder.Services.AddOdataConfiguration();

//http client used by task summary service
builder.Services.AddHttpClient("CatFact", httpClient =>
{
    httpClient.BaseAddress = new Uri("https://catfact.ninja/");
});

//using an in-memory database for testing
//TODO use a real database for production
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseInMemoryDatabase("TaskManagement"));

builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
builder.Services.AddValidatorsFromAssemblyContaining<CreateUpdateUserRequestValidator>();

var app = builder.Build();

// TODO: Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseCors(options => 
    options.AllowAnyMethod() 
       .AllowAnyHeader()
       .WithOrigins("http://localhost:4200")
   ); 

app.UseEndpoints(endpoints => endpoints.MapControllers());

app.UseGlboalExeception();

// Seed database
using (var serviceScope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
{
    var db = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    DbSeederHelper.SeedDb(db);
}

app.Run();

