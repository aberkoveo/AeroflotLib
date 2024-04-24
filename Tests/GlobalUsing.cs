using FC12.SupportExtensions.Models;
using Microsoft.Office.Interop.Outlook;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Integra.Client;
using System.Net.Http;
using System.Windows.Forms;

namespace Tests
{
    public static class GlobalUsing
    {
        public static SupportRequestDto TestingRequest1 = new SupportRequestDto()
        {
            Recipient = "Артем",
            CC = "Евген",
            BatchId = "1",
            BatchName = "BatchName_TEST",
            DocumentsIds = "1",
            RequestPriority = RequestPriority.Urgent,
            Comment = "Тестовое описание",
            SMID = 123123123
            
        };
        

        public static SupportRequestDto TestingRequest2 = new SupportRequestDto()
        {
            Recipient = "Артем",
            CC = "Евген",
            BatchId = "1",
            BatchName = "BatchName_TEST",
            
        };


        public static SupportRequestClient commonSupportClient = new SupportRequestClient(
            "http://192.168.194.92/Integra/",
            new HttpClient());


        public static SupportRequestDto TestingRequest3 = new SupportRequestDto()
        {
            BatchId = "123",
            BatchName = "Name",
            BatchOwner = "Owner",
            Recipient = "Rec",
            CC = "CC",
            DocumentsIds = "1",
            RequestPriority = RequestPriority.Low,
            



        };
    }
}
