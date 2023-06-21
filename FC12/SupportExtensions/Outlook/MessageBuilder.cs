using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using ABBYY.FlexiCapture;
using Microsoft.Office.Interop.Outlook;
using Newtonsoft.Json;
using OutlookInterop = Microsoft.Office.Interop.Outlook;
using FC12.SupportExtensions.Models;

namespace FC12.SupportExtensions.Outlook
{
    public class MessageBuilder
    {
        private readonly OutlookInterop.MailItem _mailItem;
        private readonly OutlookInterop.Application _emailApp = new OutlookInterop.Application();

        public MessageBuilder(SupportRequest request)
        {
            _emailApp = new OutlookInterop.Application();
            _mailItem = _emailApp.CreateItem(OutlookInterop.OlItemType.olMailItem) as OutlookInterop.MailItem;
            _mailItem.Subject = request.Subject;
            _mailItem.Recipients.Add(request.Recipient);
            _mailItem.CC = request.CC;
            BuildMessageBody(request);
        }

        public OutlookInterop.MailItem GetMailItem() => _mailItem;

        private void BuildMessageBody(SupportRequest request)
        {
            string bodyText = GetBody();
            string categoriesText = request.Categories;
            _mailItem.BodyFormat = OutlookInterop.OlBodyFormat.olFormatHTML;
            _mailItem.HTMLBody = String.Format(bodyText,
                categoriesText, request.BatchId, request.BatchName, request.Comment);
        }

        private string GetBody()
        {
            string[] BodyArray = Mail.Body;
            return String.Join("", BodyArray);
        }
    }
}
