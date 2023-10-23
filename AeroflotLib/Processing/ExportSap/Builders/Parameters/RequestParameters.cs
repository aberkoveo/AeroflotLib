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
        public WebHeaderCollection Headers { get; set; }
        public string ContentType { get; set; }
        public string Accept { get; set; }
        public string Method { get; set; }
        public Credentials SapCredentials { internal get; set; }
        public string SapEndPoint 
        { 
            get
            {
                return SapEndPoint + new Guid();
            }
            set
            {
                SapEndPoint = value;
            } 
        }


    }


    public class Credentials
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }

    
}
