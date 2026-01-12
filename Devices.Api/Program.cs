using Devices.Api.Models;
using Devices.Infrastructure.Data;
using Devices.Infrastructure.Repositories;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Serilog
builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));

// Database Context
builder.Services.AddDbContext<DevicesDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"));
});

// Repositories
builder.Services.AddScoped<IDeviceRepository, DeviceRepository>();

// Controllers and Validation
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
});
builder.Services.AddScoped<IValidator<DeviceCreateDto>, DeviceCreateValidator>();
builder.Services.AddScoped<IValidator<DeviceUpdateDto>, DeviceUpdateValidator>();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => 
{ 
    var xml = Path.Combine(AppContext.BaseDirectory, "Devices.Api.xml"); 
    c.IncludeXmlComments(xml, includeControllerXmlComments: true); 
});

// Health checks
builder.Services.AddHealthChecks().AddDbContextCheck<DevicesDbContext>();

// Add services to the container.

builder.Services.AddControllers();

var app = builder.Build();

// Migrate on startup
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<DevicesDbContext>();
    dbContext.Database.Migrate();              // creates DevicesDb automatically
    await DbSeeder.SeedAsync(dbContext);       // seeds data so API works first try
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSerilogRequestLogging();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.MapHealthChecks("/health");

app.Run();

public partial class Program { }