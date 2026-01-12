using Devices.Infrastructure.Data;
using Devices.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Serilog;
using FluentValidation;
using Devices.Api.Models;

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

// Swagger and OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddOpenApi();

// Health checks
builder.Services.AddHealthChecks().AddDbContextCheck<DevicesDbContext>();

// Add services to the container.

builder.Services.AddControllers();

var app = builder.Build();

// Migrate on startup
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<DevicesDbContext>();
    dbContext.Database.Migrate();
    await DbSeeder.SeedAsync(dbContext);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSerilogRequestLogging();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.MapHealthChecks("/health");

app.Run();
