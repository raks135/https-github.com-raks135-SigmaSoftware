using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Serilog;
using SigmaSoftware.API.Configurations;
using SigmaSoftware.Application.Validators;
using System.Configuration;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console(Serilog.Events.LogEventLevel.Information)
    .CreateBootstrapLogger();
Log.Information("Application Starting up.");

try
{
    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.
    builder.Services.AddControllers();
    //  builder.Services.AddFluentValidationAutoValidation();
    builder.Services.AddValidatorsFromAssemblyContaining<CandidateDtoValidator>();

    // Call the method to configure the database provider
    builder.Services.AddDatabaseConfiguration(builder.Configuration);


    // Configure Dependency Injection
    builder.ConfigDI();

    //Cache
    builder.Services.AddMemoryCache();

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
}
catch (Exception ex)
{
    Log.Fatal(ex, "Unhandled exception");

}
finally
{
    Log.Information("Shut down complete");
    Log.CloseAndFlush();
}
