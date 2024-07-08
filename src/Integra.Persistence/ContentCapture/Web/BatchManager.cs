using Integra.Persistence.Settings;
using Microsoft.Extensions.Options;
using Integra.Domain.ContentCapture;
using ContentCaptureApi;
using Integra.Persistence.FileSystem;
using Integra.Persistence.Utils;
using Integra.Persistence.ContentCapture;
using System.Reflection.Metadata;
using Document = ContentCaptureApi.Document;
namespace Integra.Persistence.ContentCapture.Web
{

    /// <summary>
    /// Реализует функцию загрузки пакетов с документами, образами документов
    /// в систему ContentCapture и запускает пакеты в обработку
    /// </summary>
    public class BatchManager : ApplicationApi, IBatchManager
    {
        private readonly int RoleTypeId = 12; // External user (see API documentation)
        private readonly int StationTypeId = 10; // External station (see API documentation)

        //private int ProjectId { get; set; }

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

        public async Task<int> HandleBatchAsync(ContentBatch batchDto)
        {
            ContentCaptureApi.Batch ccbatch = BatchBuilder.BuildBatch(batchDto);

            try
            {
                int sessionId = External_OpenSession();

                int batchId = _api.AddNewBatch(sessionId, batchDto.ProjectId, ccbatch, 0);
                if (batchId <= 0) throw new Exception($"Couldn't create the batch {ccbatch.Name}");

                _api.OpenBatch(sessionId, batchId);

                string batchFolderPath = Path.Combine(_settings.ImportFolderPath, batchDto.Name);
                var batchBytes = await FilesReader.ReadFolderFilesAsync(batchFolderPath);

                foreach (KeyValuePair<string, byte[]> item in batchBytes)
                {
                    var document = new Document { BatchId = batchId };

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

            catch (Exception ex)
            {
                Logger.Error(ex.Message + ": " + ex.StackTrace);
            }

            return default;
        }

        public async Task<int> HandleBatchBase64Async(ContentBatch batchDto)
        {
            ContentCaptureApi.Batch ccbatch = BatchBuilder.BuildBatch(batchDto);

            try
            {
                int sessionId = External_OpenSession();

                int batchId = _api.AddNewBatch(sessionId, batchDto.ProjectId, ccbatch, 0);
                if (batchId <= 0) throw new Exception($"Couldn't create the batch {ccbatch.Name}");

                _api.OpenBatch(sessionId, batchId);

                //Добавляем страницу разделитель (пустой лист А4) - бизнес-требование
                var separator = new EmptyPageBase64();
                var separatordDocument = new Document { BatchId = batchId };
                var id = await _api
                    .AddNewDocumentAsync(sessionId, separatordDocument, separator.SeparatorDocument, false, -1);

                //Добавляем вложения самих документов из BASE64
                var batchBytes = await FilesReader.ReadFromBase64Async(batchDto.Base64DocumentFiles);

                foreach (KeyValuePair<string, byte[]> item in batchBytes)
                {
                    var document = new Document { BatchId = batchId };

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

            catch (Exception ex)
            {
                Logger.Error(ex.Message + ": " + ex.StackTrace);
            }

            return default;
        }

        public BatchManager(IOptions<ContentCaptureApiSettings> settings) : base(settings)
        {}

    }
}
