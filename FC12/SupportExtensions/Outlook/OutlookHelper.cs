using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ABBYY.FlexiCapture;
using OutlookInterop = Microsoft.Office.Interop.Outlook;
using FC12.SupportExtensions.Models;
namespace FC12.SupportExtensions.Outlook
{
    public static class OutlookHelper
    {

        
        public static void CreateEmailSample(SupportRequest request)
        {
            MessageBuilder builder = new MessageBuilder(request);
            OutlookInterop.MailItem emailSample = builder.GetMailItem();
            emailSample.Display(emailSample);
        }



    }
}
