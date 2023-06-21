
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ABBYY.FlexiCapture;

using FC12.SupportExtensions.Models;
using FC12.SupportExtensions;




namespace AeroflotLib.Verification
{
    public static class UserActionsHelper
    {
        public static void SendSupportEmail(IBatch batch)
        {
            SupportRequest request = SupportRequestBuilder.SupportRequestBuild(batch);
            Application.EnableVisualStyles();
            Application.Run(new SupportMessageForm(request));
        }
    }
}
