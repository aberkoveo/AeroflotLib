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
        public  static SupportRequestDto SupportRequestBuild(
            IBatch batch, string documentsIds, string userName)
        {
            SupportRequestDto request = SupportRequestBuild(batch);
            request.DocumentsIds = documentsIds;
            request.BatchOwner = userName;
            return request;
        }

        public static SupportRequestDto SupportRequestBuild(IBatch batch)
        {
            return new SupportRequestDto
            {
                BatchId = batch.Id.ToString(),
                BatchName = batch.Name,
                Recipient = GetRecipient(batch),
                CC = GetCC(batch),
                RequestPriority = RequestPriority.Low
                
                
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
