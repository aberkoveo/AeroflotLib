
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ABBYY.FlexiCapture;
using System.Net.Http;
using FC12.SupportExtensions.Models;
using FC12.SupportExtensions;
using Integra.Client;



namespace AeroflotLib.Verification
{
    public static class UserActionsHelper
    {
        public static void SendSupportEmail(IBatch batch)
        {
            string intgraURL = batch.Project.EnvironmentVariables.Get("IntegraURL");
            IntegraClient integraClient = new IntegraClient(intgraURL, new HttpClient());

            SupportRequest request = SupportRequestBuilder.SupportRequestBuild(batch);

            Application.EnableVisualStyles();
            Application.Run(new SupportMessageForm(request, integraClient));
        }
    }
}
