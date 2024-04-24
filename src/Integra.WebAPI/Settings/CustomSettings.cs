using Integra.Persistence.Settings;

namespace Integra.WebAPI.Settings
{
    public static class CustomSettings
    {
        public static IServiceCollection AddApiSettings(
    this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ContentCaptureApiSettings>(configuration
                .GetSection("ContentCaptureApiSettings"));

            services.Configure<SolmanApiSettings>(configuration
                .GetSection("SolmanApiSettings"));

            return services;
        }
    }
}
