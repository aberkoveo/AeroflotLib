using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Integra.Domain.Support;
using Integra.Persistence.Settings;
using Microsoft.Extensions.Options;
using SolutionManagerApi;

namespace Integra.Persistence.Solman
{
    public class IncidentManager : SolmanApiHelper, IIncidentManager
    {
        public IncidentManager(IOptions<SolmanApiSettings> settings) : base(settings) { }


        public async Task<string> GetGuidAsync()
        {
            try
            {
                RequestGuid request = new RequestGuid();
                var result = await _api.RequestGuidAsync(request);
                return result.RequestGuidResponse.Guid;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message + " : " + ex.StackTrace);
            }

            return null;
        }


        public async Task<string> GetSystemGuidAsync()
        {
            try
            {
                RequestSystemGuid request = new RequestSystemGuid();
                var result = await _api.RequestSystemGuidAsync(request);
                return result.RequestSystemGuidResponse.SystemGuid;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message + " : " + ex.StackTrace);
            }

            return null;
        }


        public async Task<string> CreateIncidentAsync(SupportRequest request)
        {
            try
            {
                ProcessIncident obj = await BuildProcessIncidentAsync(request);

                ProcessIncidentResponse1 response = await _api.ProcessIncidentAsync(obj);

                return response.ProcessIncidentResponse.PrdIctId;
            }
            catch(Exception ex)
            {
                Logger.Error(ex.Message + " : " + ex.StackTrace);
            }

            return null;
        }


        private async Task<IctIncidentHead> BuildHeadAsync(SupportRequest request)
        {
            return new IctIncidentHead()
            {
                IncidentGuid = await GetGuidAsync(),
                RequesterGuid = _settings.IncidentRequesterGuid,
                ProviderGuid = await GetSystemGuidAsync(),
                ShortDescription = request.Subject,
                Priority = ((ushort)request.Priority).ToString(),
                Language = "RU"
            };
        }


        private async Task<ProcessIncident> BuildProcessIncidentAsync(SupportRequest request)
        {
            return new ProcessIncident()
            {
                IctHead = await BuildHeadAsync(request),
                IctId = request.ID.ToString(),
                IctTimestamp = Convert.ToDecimal(DateTime.Now.ToString("yyyyMMddHHmmss")),
                IctStatements = new IctIncidentStatement[]
                {
                    new IctIncidentStatement()
                    {
                        TextType = "SU99",
                        Texts = new string[]
                        {
                            request.Comment
                        },
                        Language = "RU"
                    }
                }

            };
        }
    }
}
