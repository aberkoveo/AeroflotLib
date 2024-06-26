﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Integra.Domain;
using Integra.Domain.ContentCapture;
using Integra.Persistence.ContentCapture.Web;
using Microsoft.Extensions.Options;
using Moq;
using Integra.Persistence.Settings;

namespace TestsIntegra
{
    [TestClass]
    public class UnitTestIncidentManager
    {
        private readonly IOptions<ContentCaptureApiSettings> _settings;
        private readonly ContentBatch _batch;

        public UnitTestIncidentManager()
        {
            ContentCaptureApiSettings settings = new ContentCaptureApiSettings()
            {
                ApplicationServerUrl = "http://192.168.194.92",
                ImportFolderPath = "E:\\IntegraImportFolder"
            };

            var optionsMock = new Mock<IOptions<ContentCaptureApiSettings>>();
            optionsMock.Setup(v => v.Value).Returns(settings);
            _settings = optionsMock.Object;

            _batch = new ContentBatch()
            {
                BatchTypeId = 69,
                Name = "UZEDO_unittest_batch_" + "1",
                OwnerId = 162,
                ProjectId = 21
            };
        }

        [TestMethod]
        public async Task TestHandleBatch()
        {
            NLog.Common.InternalLogger.LogToConsole = true;
            NLog.Common.InternalLogger.LogLevel = NLog.LogLevel.Debug;

            BatchManager unit = new BatchManager(_settings);
            int batchId = await unit.HandleBatchAsync(_batch);
            Console.WriteLine($"batch id = {batchId}");
        }
    }
}
