using System.Reflection;
using Integra.Application;
using Integra.Application.Common.Mappings;
using Integra.Application.Interfaces;
using Integra.Persistence;
using NLog;
using NLog.Web;
using Integra.WebAPI.Settings;
using FluentValidation;
using Integra.WebApi.Controllers.ContentCapture.Validation;
using Integra.Domain.ContentCapture;
using AspNetCoreRateLimit;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

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

    builder.Services.AddAuthorization();
    builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidIssuer = AuthOptions.ISSUER,
            ValidateAudience = false,
            ValidAudience = AuthOptions.AUDIENCE,
            ValidateLifetime = false,
            IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
            ValidateIssuerSigningKey = true
        };

    });
    #region RateLimit
    builder.Services.AddMemoryCache();
    builder.Services.Configure<IpRateLimitOptions>(builder.Configuration.GetSection("IpRateLimiting"));
    builder.Services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
    builder.Services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();
    builder.Services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
    builder.Services.AddSingleton<IProcessingStrategy, AsyncKeyLockProcessingStrategy>();
    builder.Services.AddInMemoryRateLimiting();
    #endregion RateLimit

    var app = builder.Build();

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseDefaultFiles();
    app.UseStaticFiles();
    app.UseAuthentication();
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

