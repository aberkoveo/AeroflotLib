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

//#warning ТЕХДОЛГ!! Добавить Автора инцидента
        public int IncidentAuthorId { get; set; }

        //public int IncidentExecutorId { get; set; }

        public string IncidentRequesterGuid { get; set; } 

        public string SAPProcessType { get; set; }

        public string SAPMultiLevelCategoryID { get;set; } //Функцкиональное направление

        public string SapSolutionType { get; set; }
    }
}
