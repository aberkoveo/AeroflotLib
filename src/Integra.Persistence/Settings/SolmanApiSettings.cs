using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integra.Persistence.Settings
{
    /// <summary>
    /// 
    /// </summary>
    public class SolmanApiSettings
    {
        /// <summary>
        /// Корневой путь к серверу ABAP, где доступно API Solman, с портом, например
        /// http://localhost:8000
        /// </summary>
        public string ServiceUrl { get; set; }

        /// <summary>
        /// Пользователь, под которым происходит аутентификация API Solman
        /// </summary>
        public string ServiceUser { get; set; }

        /// <summary>
        /// Пароль пользователя, под которым происходит аутентификация API Solman
        /// </summary>
        public string ServicePassword { get; set; }

        /// <summary>
        /// SAP Guid системы, откуда выполняются запросы к API Solman
        /// </summary>
        public string IncidentRequesterGuid { get; set; }

        /// <summary>
        /// Соответствует полю "Тип обращения" в создаваемых обращениях
        /// </summary>
        public string SAPProcessType { get; set; }

        /// <summary>
        /// Соответствует полю "Функцкиональное направление" в создаваемых обращениях
        /// </summary>
        public string SAPMultiLevelCategoryID { get;set; }

        /// <summary>
        /// Соответствует полю "SAP-Решение" в создаваемых обращениях
        /// </summary>
        public string SapSolutionType { get; set; }
    }
}
