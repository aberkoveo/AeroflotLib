using FC12.SupportExtensions.Models;
using Microsoft.Office.Interop.Outlook;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    public static class GlobalUsing
    {
        public static SupportRequest TestingRequest1 = new SupportRequest()
        {
            Recipient = "Артем",
            CC = "Евген",
            BatchId = "1",
            BatchName = "BatchName_TEST",
            Priority = Priority.Urgent,
            Comment = "Тестовое описание"
            
        };
        

        public static SupportRequest TestingRequest2 = new SupportRequest()
        {
            Recipient = "Артем",
            CC = "Евген",
            BatchId = "1",
            BatchName = "BatchName_TEST",
            
        };


    }
}
