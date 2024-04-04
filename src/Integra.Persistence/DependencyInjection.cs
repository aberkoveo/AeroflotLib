using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Integra.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Integra.Persistence.Settings;
using Microsoft.Extensions.Options;

namespace Integra.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services
        , IConfiguration configuration)
    {
        var connectionString = configuration["AbbyyDBConnectionString"];
        services.AddDbContext<SupportRequestDBContext>(opts => { opts.UseSqlServer(connectionString); });

        services.AddScoped<ISupportRequestDBContext>(provider =>
            provider.GetService<SupportRequestDBContext>());


        return services;
    }
}