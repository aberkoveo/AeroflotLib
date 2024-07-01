using Integra.Domain.Support;
using Integra.Persistence.Settings;
using Integra.Persistence.Utils;
using Microsoft.Extensions.Options;
using SolutionManagerApi;

namespace Integra.Persistence.Solman
{

    /// <summary>
    /// Тип реализует функционал по созданию инцидентов в Solman через его API
    /// </summary>
    public class IncidentManager : SolmanApiHelper, IIncidentManager
    {
        public IncidentManager(IOptions<SolmanApiSettings> settings) : base(settings) { }


        public async Task<string> GetGuidAsync()
        {
            try
            {
                RequestGuid request = new RequestGuid();
                var result = await _api.RequestGuidAsync(request);
                if (String.IsNullOrEmpty(result.RequestGuidResponse.Guid))
                {
                    throw new Exception("Cant get GUID from SolMan API..");
                }
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

                Logger.Debug("ProcessIncident object: \n" + JsonWriter.ConvertObject(obj));

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
                            "Имя пользователя: " + request.BatchOwner,
                            "Категории: " + request.Categories,
                            "Идентификатор пакета: " + request.BatchId,
                            "Имя пакета: " + request.BatchName,
                            "Идентификаторы документов: " + request.DocumentsIds,
                            "Комментарий: " + request.Comment
                        },
                        Language = "RU"
                    }
                },
                IctAdditionalInfos = new IctIncidentAdditionalInfo[]
                {
                    new IctIncidentAdditionalInfo()
                    {
                        AddInfoAttribute = "SAPMultiLevelCategoryID",
                        AddInfoValue = _settings.SAPMultiLevelCategoryID
                    },
                    new IctIncidentAdditionalInfo()
                    {
                        AddInfoAttribute = "SAPProcessType",
                        AddInfoValue = _settings.SAPProcessType
                    },
                    new IctIncidentAdditionalInfo()
                    {
                        AddInfoAttribute = "SapSolutionType",
                        AddInfoValue = _settings.SapSolutionType
                    }

                },
                IctAttachments = new IctIncidentAttachment[] { },
                IctPersons = new IctIncidentPerson[] { },
                IctSapNotes = new IctIncidentSapNote[] { },
                IctSolutions = new IctIncidentSolution[] { },
                IctUrls = new IctIncidentUrl[] { }
            };
        }
    }
}
