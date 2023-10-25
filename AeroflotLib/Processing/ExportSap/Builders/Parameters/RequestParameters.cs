using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AeroflotLib.Processing.ExportSap.Factories.Parameters
{
    public class RequestParameters
    {
        public WebHeaderCollection Headers { get; set; } = new WebHeaderCollection();
        public string ContentType { get; set; }
        public string Accept { get; set; }
        public string Method { get; set; }
        public string SapLogin { internal get; set; }
        public string SapPassword { internal  get; set; }

        private string sapEndpoint;
        public string SapEndpoint 
        { 
            get
            {
                return sapEndpoint + new Guid();
            }
            set
            {
                sapEndpoint = value;
            } 
        }


    }



    
}
