using ABBYY.FlexiCapture;
using AeroflotLib.Processing.ExportSap.Factories.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FC12;
using System.Net;


namespace AeroflotLib.Processing.ExportSap.Builders.Parameters
{
    public static class SapRequestParametersBuilder
    {
        public static SapRequestParameters BuildSapRequestParameters(IDocument document)
        {

            IProject project = document.Batch.Project;
            string sapEndpoint = Utilities.GetEnvironmentVariable(ProjectEnvironmentProperties.SapEndpoint, project);
            string sapLogin = Utilities.GetEnvironmentVariable(ProjectEnvironmentProperties.SapLogin, project);
            string sapPassword = Utilities.GetEnvironmentVariable(ProjectEnvironmentProperties.SapPassword, project);

            SapRequestParameters parameters = new SapRequestParameters
            {
                SapEndpoint = sapEndpoint,
                SapLogin = sapLogin,
                SapPassword = sapPassword
            };

            parameters.AddAuthHeaders();


            return parameters;


        }
    }
}
