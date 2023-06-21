using Integra.Persistence;
using System.Reflection;
using Integra.Application.Common.Mappings;
using Integra.Application.Interfaces;
using Integra.Application;


namespace Integra.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddAutoMapper(config =>
            {
                config.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));
                config.AddProfile(new AssemblyMappingProfile(typeof(ISupportRequestDBContext).Assembly));
            });

            builder.Logging.AddEventLog(eventLogSettings =>
            {
                eventLogSettings.SourceName = "Integra";
                eventLogSettings.LogName = "IntegraLog";
            });

            builder.Services.AddApplication();
            builder.Services.AddPersistence(builder.Configuration);
            builder.Services.AddControllers();
            
            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var serviceProvider = scope.ServiceProvider;
                var context = serviceProvider.GetRequiredService<SupportRequestDBContext>();
                DbInitializer.Initialize(context);
            }

            //app.UseSwagger();
            //app.UseSwaggerUI();
            app.MapControllers();
            app.Run();
            //app.UseCors("AllowAll");

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapControllers();
            //});
            app.UseRouting();

            //app.UseAuthorization();

            




        }
    }
}