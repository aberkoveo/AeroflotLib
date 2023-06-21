using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Integra.Application.Interfaces;

namespace Integra.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services
            , IConfiguration configuration)
        {
            var connectionString = configuration["AbbyyDBConnectionString"];
            services.AddDbContext<SupportRequestDBContext>(opts =>
            {
                opts.UseSqlServer(connectionString);
            });
            services.AddScoped<ISupportRequestDBContext>(provider => 
                provider.GetService<SupportRequestDBContext>());
            return services;
        }
    }
}
