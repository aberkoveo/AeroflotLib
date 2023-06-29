using System.Reflection;
using Integra.Application;
using Integra.Application.Common.Mappings;
using Integra.Application.Interfaces;
using Integra.Persistence;
using NLog;
using NLog.Web;

var logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
logger.Info("Запуск сервиса интеграции");

try
{
    var builder = WebApplication.CreateBuilder(args);
    
    // Configure logging
    builder.Logging.ClearProviders();
    builder.Host.UseNLog();
    // Add services to the container.
    
    builder.Services.AddAutoMapper(config =>
    {
        config.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));
        config.AddProfile(new AssemblyMappingProfile(typeof(ISupportRequestDBContext).Assembly));
    });
    

    builder.Services.AddApplication();
    builder.Services.AddPersistence(builder.Configuration);
    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    var app = builder.Build();
    
    using (var scope = app.Services.CreateScope())
    {
        var serviceProvider = scope.ServiceProvider;
        var context = serviceProvider.GetRequiredService<SupportRequestDBContext>();
        DbInitializer.Initialize(context);
    }
    

    // Configure the HTTP request pipeline.
    app.UseSwagger();
    app.UseSwaggerUI();

    //app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
catch (Exception exception)
{
    logger.Error(exception, "Остановка программы из-за исключения");
}
finally
{
    LogManager.Shutdown();
}

