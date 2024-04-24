using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Integra.Domain;
using Integra.Domain.ContentCapture;
using Integra.Persistence.Solman;
using Microsoft.Extensions.Options;
using Moq;
using Integra.Persistence.Settings;

namespace TestsIntegra.Solman
{
    [TestClass]
    public class UnitTestIncidentManager
    {
        private readonly IOptions<SolmanApiSettings> _settings;

        public UnitTestIncidentManager()
        {
            SolmanApiSettings settings = new SolmanApiSettings()
            {
                ServiceUrl = "http://mlk-sm-02.msk.aeroflot.ru:8000",
                ServiceUser = "INC_TEST",
                ServicePassword = "@WSX2wsx"
            };

            var optionsMock = new Mock<IOptions<SolmanApiSettings>>();
            optionsMock.Setup(v => v.Value).Returns(settings);
            _settings = optionsMock.Object;

            
        }

        [TestMethod]
        public async Task TestGetGuid()
        {
            NLog.Common.InternalLogger.LogToConsole = true;
            NLog.Common.InternalLogger.LogLevel = NLog.LogLevel.Info;

            IncidentManager unit = new IncidentManager(_settings);
            string guid = await unit.GetGuidAsync();
            Console.WriteLine($"guid = {guid}");

            Assert.AreEqual( 32, guid.Length );
        }

        
    }
}
