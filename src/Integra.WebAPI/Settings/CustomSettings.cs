using Integra.Persistence.Settings;

namespace Integra.WebAPI.Settings
{

    /// <summary>
    /// Добавление дополнительных конфигураций для прикладных сервисов
    /// из appsettings
    /// </summary>
    public static class CustomSettings
    {
        public static IServiceCollection AddApiSettings(
    this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ContentCaptureApiSettings>(configuration
                .GetSection("ContentCaptureApiSettings"));

            services.Configure<SolmanApiSettings>(configuration
                .GetSection("SolmanApiSettings"));

            services.Configure<AuthenticationSettings>(configuration
                .GetSection("AuthenticationSettings"));

            return services;
        }
    }
}
