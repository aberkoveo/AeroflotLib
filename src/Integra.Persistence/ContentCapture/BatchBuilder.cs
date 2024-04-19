using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ABBYY.FlexiCapture;
using ContentCaptureApi;
using Integra.Domain.ContentCapture;

using ContentCaptureApi;

namespace Integra.Persistence.ContentCapture
{
    public static class BatchBuilder
    {
        public static Batch BuildBatch(ContentBatch dto)
        {
            var resultBatch = new Batch
            {
                Name = dto.Name,
                ProjectId = dto.ProjectId,
                OwnerId = dto.OwnerId,
                BatchTypeId = dto.BatchTypeId,
            };

            BuildProperties(resultBatch, dto.RegistrationParameters);

            return resultBatch;
        }

        public static void BuildProperties(this Batch batch,
            Dictionary<string, string> input)
        {
            if (input.Any())
            {
                batch.Properties = new Batch.PropertiesType();

                foreach (string key in input.Keys)
                {
                    batch.Properties.Add(new RegistrationProperty
                    {
                        Name = key,
                        Value = input[key]
                    });
                }
            } else
            {
                throw new Exception($"В пакете {batch.Name} в передаваемом списке рег. параметров нет элементов"); 
            }
        }
    }
}
