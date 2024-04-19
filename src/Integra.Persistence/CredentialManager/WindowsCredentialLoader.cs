using Microsoft.Extensions.Options;
//using Microsoft.Identity.Client.Platforms.Features.DesktopOs.Kerberos;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CredentialManagement;
using Integra.Persistence.Settings;

namespace Integra.Persistence.CredentialManager
{
    public class WindowsCredentialLoader : IWindowsCredentialLoader
    {
        private readonly ILogger _logger;
        private readonly ContentCaptureApiSettings _settings;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="settings">модель настроек для сервиса отправки уведомлений верификации</param>
        public WindowsCredentialLoader(IOptions<ContentCaptureApiSettings> settings)
        {
            _logger = LogManager.GetLogger("VerifNotifyLogger");
            _settings = settings.Value;
        }

        /// <summary>
        /// Загружает Windows generic credential из ОС локально
        /// </summary>
        /// <returns>модель учетных данных credential</returns>
        public Credential Load()
        {

            var credential = new Credential();
            //credential.Target = _settings.;
            credential.Load();

            if (credential.Exists())
            {
                _logger.Debug($"Credential {credential.Target} has received successfully.");
            }
            else
            {
                _logger.Error($"Credential {credential.Target} is NOT accessible.");
            }

            return credential;
        }
    }
}
