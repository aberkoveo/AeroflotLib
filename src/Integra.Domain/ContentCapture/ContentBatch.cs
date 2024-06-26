﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integra.Domain.ContentCapture
{
    /// <summary>
    /// Класс реализует основную единицу передачи информации
    /// в интеграции с API ContentCapture
    /// </summary>
    [Serializable]
    public class ContentBatch
    {
        public string Name { get; set; }

        public int ProjectId { get; set; }

        public int BatchTypeId { get; set; }

        public int OwnerId { get; set; }

        public Dictionary<string, string> RegistrationParameters { get; set; }

        public Dictionary<string, string> Base64DocumentFiles { get; set; }
    }
}