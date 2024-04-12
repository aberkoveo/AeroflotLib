using Integra.Persistence.Settings;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;
using Integra.Domain;
using Integra.Domain.ContentCapture;
using ABBYY.FlexiCapture;
using ContentCaptureApi;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

using Integra.Persistence.FileSystem;

namespace Integra.Persistence.ContentCapture
{
    public class BatchManager : ApplicationApi, IBatchManager
    {
        private int RoleTypeId = 12; // External user (see API documentation)
        private int StationTypeId = 10; // External station (see API documentation)

        private int ProjectId { get; set; }

        public int External_OpenSession()
        {
            int sessionId = _api.OpenSession(RoleTypeId, StationTypeId);

            if (sessionId == 0)
            {
                throw new Exception("Cant open user session!");
            }

            return sessionId;
        }

        public void External_CloseSession(int sessionId)
        {
            if (sessionId == 0)
            {
                return;
            }

            _api.CloseSession(sessionId);
        }

        public async Task<int> HandleBatchAsync(ContentBatch contentBatch)
        {
            var ccbatch = new Batch 
            { 
                Name = contentBatch.Name, 
                OwnerId = contentBatch.OwnerId,
                BatchTypeId = contentBatch.BatchTypeId
            };

            try
            {
                int sessionId = External_OpenSession();

                int batchId = _api.AddNewBatch(sessionId, ProjectId, ccbatch, contentBatch.OwnerId);
                if (batchId <= 0) throw new Exception($"Couldn't create the batch {ccbatch.Name}");

                _api.OpenBatch(sessionId, batchId);

                string batchFolderPath = Path.Combine(_settings.ImportFolderPath, contentBatch.Name);
                var batchBytes = await FilesReader.ReadFolderFilesAsync(batchFolderPath);

                foreach (KeyValuePair<string, byte[]> item in batchBytes)
                {
                    var document = new ContentCaptureApi.Document {BatchId = batchId};

                    var documentFile = new ContentCaptureApi.File()
                    {
                        Name = item.Key,
                        Bytes = item.Value
                    };

                    Logger.Debug($"Loaded file {item.Key}");

                    var documentId = await _api
                        .AddNewDocumentAsync(sessionId, document, documentFile, false, -1);
                }

                _api.CloseBatch(sessionId, batchId);
                
                _api.ProcessBatch(sessionId, batchId);

                _api.CloseSession(sessionId);

                Logger.Info($"Started processing the batch with ID={batchId}");

                return batchId;
            }

            catch ( Exception ex)
            {
                Logger.Error(ex.Message + ": " + ex.StackTrace);
            }

            return default;
        }

        public BatchManager(IOptions<ApiSettingsModel> settings) : base(settings)
        {
            ProjectId = _settings.ProjectId;
        }

    }
}
