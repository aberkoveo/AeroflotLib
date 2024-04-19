
using ContentCaptureApi;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using NLog;
using Integra.Persistence.Settings;


namespace Integra.Persistence.ContentCapture.Web
{
    public class ApplicationApi
    {
        protected ILogger Logger => LogManager.GetCurrentClassLogger();

        protected readonly FlexiCaptureWebServiceSoapClient _api;
        protected readonly ContentCaptureApiSettings _settings;

        protected ApplicationApi(IOptions<ContentCaptureApiSettings> settings)
        {
            _settings = settings.Value;
            _api = SetupApiClient();
        }


        private FlexiCaptureWebServiceSoapClient SetupApiClient()
        {
            var endpoint = new EndpointAddress(_settings.ApplicationServerUrl + "/ContentCapture12/server/API/v1/SOAP");
            BasicHttpBinding binding = new BasicHttpBinding();
            binding.Security.Transport.ClientCredentialType = HttpClientCredentialType.Windows;
            binding.Security.Mode = BasicHttpSecurityMode.TransportCredentialOnly;

            var apiClient = new FlexiCaptureWebServiceSoapClient(binding, endpoint);

            try
            {
                TestApiUserAccess(apiClient);
            }
            catch (Exception ex)
            {
                Logger.Error($"ContentCapture connection has not been successfull: {ex.Message}");
            }


            return apiClient;
        }


        /// <summary>
        /// Проверяет права доступа исполняющего пользователя (от имени которого 
        /// работает сервис или запускается работа приложения) в системе - для 
        /// корректной работы необходмы права администратора ContentCapture
        /// </summary>
        /// <param name="api">Клиент сервиса управления Сервером приложений</param>
        /// <exception cref="UserAccessException"></exception>
        private void TestApiUserAccess(FlexiCaptureWebServiceSoapClient api)
        {
            string me = WindowsIdentity.GetCurrent().Name;
            string testData = api.FindUserAsync(me).Result.ToString();

            if (!String.IsNullOrEmpty(testData))
            {
                Logger.Debug($"Current user {me} has access to ContentCapture API");
            }
            else
            {
                throw new Exception($"Current user {me} has NO ACCESS to ContentCapture API");
            }

        }
    }
}
