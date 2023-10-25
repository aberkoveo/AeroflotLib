using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AeroflotLib.Processing.ExportSap.Factories.Parameters
{
        public class SapRequestParameters : RequestParameters
        {
        
        
            public SapRequestParameters()
            {
                Headers.Add(@"SOAPAction:urn:sap-com:document:sap:rfc:functions:ZFM_ABBYY_FILE_SAVE:ZFM_ABBYY_FILE_SAVERequest");
                ContentType = "text/xml;charset=\"utf-8\"";
                Accept = "text/xml";
                Method = "POST";
                
            }

        public void AddAuthHeaders()
        {
            Headers.Add("Authorization", "Basic " + EncodedCredentials);
        }
        

            private string EncodedCredentials
            {
                get
                {
                    return Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(
                        SapLogin + ":" +
                        SapPassword));
                }
            }

        }
}
