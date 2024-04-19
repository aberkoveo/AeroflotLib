using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Integra.Domain;
using Integra.Domain.ContentCapture;
using Integra.Persistence.ContentCapture;
using Microsoft.Extensions.Options;
using Moq;
using Integra.Persistence.ContentCapture;
using Newtonsoft.Json.Linq;

namespace TestsIntegra
{
    [TestClass]
    public class UnitTestPropertiesBuilder
    {
        

        public UnitTestPropertiesBuilder()
        {
            
        }

        //[TestMethod]
        //public async Task TestBuildProperties()
        //{
        //    ContentBatch input = new ContentBatch()
        //    {
        //        Name = "test",
        //        ProjectId = 1,
        //        OwnerId = 1,
        //        RegistrationParameters = new (string name, string value)[]
        //        {
        //            new(){ name = "propName1",value = "propValue1"},
        //            new(){ name = "propName2",value = "propValue2"}

        //        }

        //    };

        //    var result = BatchBuilder.BuildBatch(input);

        //    Console.WriteLine(JsonWriter.ConvertObject(input));
        //    Console.WriteLine(JsonWriter.ConvertObject(input.RegistrationParameters.First().name));
        //    Console.WriteLine("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
        //    Console.WriteLine(result.Properties[0].Name);
        //    Console.WriteLine(JsonWriter.ConvertObject(result));

        //}
    }
}
