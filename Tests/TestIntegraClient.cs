using FC12.SupportExtensions.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Integra.Client;
using System.Net.Http;

namespace Tests
{
    [TestClass]
    public class TestIntegraClient
    {
        [TestMethod]
        public async Task TestCreateIncident()
        {
            string url = "http://192.168.194.92/Integra/";

            var supportClient = new SupportRequestClient(url, new HttpClient());

            var req = new SupportRequest
            {
                BatchId = "ИД",
                BatchName= "ИМЯ",
                BatchOwner = "Овнер",
                Categories= "Исключения",
                CC = "СС",
                Comment = "Комметнрий",
                CreationDate = DateTime.Now.ToString(),
                Priority = (Priority)(int)RequestPriority.Urgent,
                Subject = "1",
                Recipient = "1",
                ID = 1,
                DocumentsIds = "1,2"
                
                
                
                

            };
            
            int result  = await supportClient.CreateIncidentAsync(req);
            Console.WriteLine(result);
        }
    }
}
