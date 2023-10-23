using ABBYY.FlexiCapture;
using AeroflotLib.Processing.ExportSap.Factories.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AeroflotLib.Processing.ExportSap.Builders.Parameters
{
    public static class SapRequestParametersBuilder
    {
        public static SapRequestParameters BuildSapRequestParameters(IDocument document)
        {
            


            return new SapRequestParameters
            {
                SapEndPoint = document.Properties.Get(ProjectEnvironmentProperties.SapEnpoint),
                SapCredentials = new Credentials
                {
                    Login = document.Properties.Get(ProjectEnvironmentProperties.SapLogin),
                    Password = document.Properties.Get(ProjectEnvironmentProperties.SapPassword)
                }

            };
        }
    }
}
