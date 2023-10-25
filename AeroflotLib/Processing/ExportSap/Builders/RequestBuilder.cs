using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Threading.Tasks;
using ABBYY.FlexiCapture;


namespace AeroflotLib.Processing.ExportSap.Factories.Parameters
{
    public static class RequestBuilder
    {
        public static HttpWebRequest CreateSapRequest(SapRequestParameters parameters)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(parameters.SapEndpoint);
            request.Headers = parameters.Headers;
            request.Method = parameters.Method;    
            request.Accept = parameters.Accept;
            request.ContentType = parameters.ContentType;
            return request;
        }






    }
}
