using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integra.Persistence.Settings
{
    public class SolmanApiSettings
    {
        public string ServiceUrl { get; set; }

        public string ServiceUser { get; set; }

        public string ServicePassword { get; set; }

        public int IncidentAuthorId { get; set; }

        public int IncidentExecutorId { get; set; }

        public string IncidentRequesterGuid { get; set; } 
    }
}
