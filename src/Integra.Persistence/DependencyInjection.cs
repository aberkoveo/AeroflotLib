using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Integra.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Integra.Persistence.Settings;
using Microsoft.Extensions.Options;
using Integra.Persistence.FileSystem;
using Integra.Persistence.ContentCapture;

namespace Integra.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services
        , IConfiguration configuration)
    {
        var connectionString = configuration
            .GetSection("ApiSettings")
            .GetSection("DBConnectionString").Value;

        services.AddDbContext<SupportRequestDBContext>(opts => { opts.UseSqlServer(connectionString); });

        services.AddScoped<ISupportRequestDBContext>(provider =>
            provider.GetService<SupportRequestDBContext>());

        services.AddScoped<IBatchManager, BatchManager>();

        return services;
    }
}