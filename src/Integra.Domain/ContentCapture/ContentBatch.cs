using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integra.Domain.ContentCapture
{
    [Serializable]
    public class ContentBatch
    {
        public string Name { get; set; }

        public int ProjectId { get; set; }

        public int BatchTypeId { get; set; }

        public int OwnerId { get; set; }

        public Dictionary<string, string> RegistrationParameters { get; set; }
    }
}