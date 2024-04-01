using ABBYY.FlexiCapture;
using FC12.SupportExtensions.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FC12.SupportExtensions
{
    public static class SupportRequestBuilder 
    {
        public  static SupportRequest SupportRequestBuild(IBatch batch, string[] documentsIds)
        {
            SupportRequest request = SupportRequestBuild(batch);
            request.DocumentsIds = documentsIds;
            return request;
        }

        public static SupportRequest SupportRequestBuild(IBatch batch)
        {
            return new SupportRequest
            {
                BatchId = batch.Id.ToString(),
                BatchName = batch.Name,
                BatchOwner = batch.CreatedBy.Name,
                Recipient = GetRecipient(batch),
                CC = GetCC(batch),
                DocumentsIds = new[] { "" }
            };
        }

        private static string GetRecipient(IBatch batch)
        {
            return Utilities.GetEnvironmentVariable("SupportMailAdress", batch.Project);
        }

        private static string GetCC(IBatch batch)
        {
            return Utilities.GetEnvironmentVariable("SupportMailAdressCC", batch.Project);
        }
    }
}
