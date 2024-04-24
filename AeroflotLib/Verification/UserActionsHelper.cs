
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ABBYY.FlexiCapture;
using System.Net.Http;
using SupportRequestDto = FC12.SupportExtensions.Models.SupportRequestDto;
using FC12.SupportExtensions;
using Integra.Client;



namespace AeroflotLib.Verification
{
    public static class UserActionsHelper
    {
        public static void ExecuteSupportRequest(IBatch batch, string documentsIds, string userName)
        {
            try
            {
                string intgraURL = batch.Project.EnvironmentVariables.Get("IntegraURL");
                SupportRequestClient SupportRequestClient = new SupportRequestClient(intgraURL, new HttpClient());

                SupportRequestDto request = SupportRequestBuilder.SupportRequestBuild(
                    batch, documentsIds, userName);

                Application.EnableVisualStyles();
                Application.Run(new SupportMessageForm(request, SupportRequestClient));

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " : " + ex.StackTrace);
            }
            


        }
    }
}
