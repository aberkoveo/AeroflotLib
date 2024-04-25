using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integra.Persistence.Settings
{
    /// <summary>
    /// Модель конфигурации для интеграции с ContentCaptureApi
    /// </summary>
    public class ContentCaptureApiSettings
    {
        /// <summary>
        /// Подключение к СУБД, где должна быть создана таблица SupportRequests
        /// </summary>
        public string DBConnectionString { get; set; }

        /// <summary>
        /// Корневой путь к серверу приложений ContentCapture,
        /// например http://localhost
        /// </summary>
        public string ApplicationServerUrl { get; set; }

        /// <summary>
        /// Идентификатор проекта, где будут создаваться пакеты обрбаботки
        /// </summary>
        public int ProjectId { get; set; }

        /// <summary>
        /// Корневой каталог импорта, откуда будут читаться файлы документов 
        /// для отправки их на распознавание и обработку в ContentCapture.
        /// </summary>
        public string ImportFolderPath { get; set; }
    }
}
