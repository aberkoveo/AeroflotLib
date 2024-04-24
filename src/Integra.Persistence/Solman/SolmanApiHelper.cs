using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Security;
using System.Text;
using System.Threading.Tasks;

using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceModel.Security;
using Integra.Persistence.Settings;
using SolutionManagerApi;
using Microsoft.Extensions.Options;
using NLog;
using ILogger = NLog.ILogger;

namespace Integra.Persistence.Solman
{
    public class SolmanApiHelper
    {

        protected readonly CT_SERVICE_DESK_APIClient _api;
        protected ILogger Logger => LogManager.GetLogger("SolmanLogger");
        //protected ILogger Logger => LogManager.GetCurrentClassLogger();
        protected readonly SolmanApiSettings _settings;

        public SolmanApiHelper(IOptions<SolmanApiSettings> settings)
        {
            _api = SetupApiClient(settings.Value);
            _settings = settings.Value;
        }


        private CT_SERVICE_DESK_APIClient SetupApiClient(SolmanApiSettings settings)
        {
            string endpointPath = "/sap/bc/srt/rfc/sap/ict_service_desk_api/800" +
                "/ict_service_desk_api/ict_service_desk_api";

            var endpoint = new EndpointAddress(settings.ServiceUrl + endpointPath);

            BasicHttpBinding binding = new BasicHttpBinding();
            binding.Security.Transport.ClientCredentialType = HttpClientCredentialType.Basic;
            binding.Security.Mode = BasicHttpSecurityMode.TransportCredentialOnly;

            var api = new CT_SERVICE_DESK_APIClient(binding, endpoint);

            api.ClientCredentials.UserName.UserName = settings.ServiceUser;
            api.ClientCredentials.UserName.Password = settings.ServicePassword;

            return api;
        }
    }
}
