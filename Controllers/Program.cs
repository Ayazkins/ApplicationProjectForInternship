using System.Text.Json.Serialization;
using Controllers.ExceptionFilters;
using DAL;
using DAL.DependencyInjection;
using Domain.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using PluginManager;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(o =>
    o.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))); 
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    options.JsonSerializerOptions.IgnoreNullValues = true;
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var a = builder.Services.GetServiceInstance<ApplicationDbContext>();
builder.Services.InitRepo();
builder.Services.AddDi();

builder.Services.AddMvc(options =>
{
    options.Filters.Add<ApplicationAlreadyCommittedExceptionFilter>();
    options.Filters.Add<NoSuchApplicationExceptionFilter>();
    options.Filters.Add<AuthorAlreadyHasApplicationExceptionFilter>();
    options.Filters.Add<NotEnoughFieldsOccuredExceptionFilter>();
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
      scope.ServiceProvider.GetRequiredService<ApplicationDbContext>().Database.Migrate();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();