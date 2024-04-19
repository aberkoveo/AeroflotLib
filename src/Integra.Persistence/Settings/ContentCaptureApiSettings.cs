using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integra.Persistence.Settings
{
    public class ContentCaptureApiSettings
    {
        public string DBConnectionString { get; set; }

        public string ApplicationServerUrl { get; set; }

        public int ProjectId { get; set; }

        public string ImportFolderPath { get; set; }
    }
}
