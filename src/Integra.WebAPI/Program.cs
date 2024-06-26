using System.Reflection;
using Integra.Application;
using Integra.Application.Common.Mappings;
using Integra.Application.Interfaces;
using Integra.Persistence;
using NLog;
using NLog.Web;
using Integra.WebAPI.Settings;
using FluentValidation;
using FluentValidation.AspNetCore;
using Integra.WebApi.Controllers.ContentCapture.Validation;
using Integra.Domain.ContentCapture;

var logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
logger.Info("Запуск сервиса интеграции");

try
{
    var builder = WebApplication.CreateBuilder(args);

    // Configure logging
    builder.Host.UseNLog();

    // Add services to the container.
    builder.Services.AddAutoMapper(config =>
    {
        config.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));
        config.AddProfile(new AssemblyMappingProfile(typeof(ISupportRequestDBContext).Assembly));
    });

    builder.Services.AddApiSettings(builder.Configuration);

    builder.Services.AddApplication();
    builder.Services.AddPersistence(builder.Configuration);
    builder.Services.AddControllers();
    builder.Services.AddApiVersioning();

    builder.Services.AddScoped<IValidator<ContentBatch>, ContentBatchValidator>();

    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    var app = builder.Build();

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }


    app.UseAuthorization();
    app.MapControllers();
    app.Run();
}
catch (Exception exception)
{
    logger.Error(exception, exception.StackTrace);
}
finally
{
    LogManager.Shutdown();
}

