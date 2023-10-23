using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

using AeroflotLib.Processing.ExportSap.RequestBody;
using AeroflotLib.Processing.ExportSap.Builders;

namespace Tests.SapExport
{
    [TestClass]
    public class TestXmlBody
    {
        private readonly Envelope xml;
        public TestXmlBody()
        {
            xml = new Envelope();
            xml.Body = new Body();
            xml.Body.ZFMABBYYFILESAVE = new ZFMABBYYFILESAVE();
            xml.Body.ZFMABBYYFILESAVE.ICONTENT = "BYTES";
            xml.Body.ZFMABBYYFILESAVE.IDOCTYPE = "3";
            xml.Body.ZFMABBYYFILESAVE.IOBJKEY = "51056332152020";
            xml.Body.ZFMABBYYFILESAVE.IOBJTYPE= "BUS2081";
        }
            

        [TestMethod]
        public void TestString()
        {

            var settings = new XmlWriterSettings
            {
                Indent = true,
                OmitXmlDeclaration = true
            };
            XmlSerializerNamespaces myNamespaces = new XmlSerializerNamespaces();
            myNamespaces.Add("urn", "urn:sap-com:document:sap:rfc:functions");
            myNamespaces.Add("soapenv", "http://schemas.xmlsoap.org/soap/envelope/");
            


            var stringwriter = new System.IO.StringWriter();
            var xmlWriter = XmlWriter.Create(stringwriter, settings);

            var serializer = new XmlSerializer(typeof(Envelope));
            serializer.Serialize(xmlWriter, xml, myNamespaces);
            Console.WriteLine( stringwriter.ToString());
        }


        [TestMethod]
        public void TestBodyBuilder()
        {
            string[] req = new string[4];
            req[0] = "JVBERi0xLjUKJeLjz9MKOSAwIG9iago8PC9UeXBlL1hPYmplY3QvU3VidHlwZS9Gb3JtL0JCb3hbMCAwIDYwOS4xIDg0NC4zXS9SZXNvdXJjZXMgMTAgMCBSL0ZpbHRl";    //I_CONTENT
            req[1] = "3";    //I_DOCTYPE optional
            req[2] = " 2020";    //I_OBJKEY
            req[3] = "BUS2081";    //I_OBJTYPE

            string body = RequestBodyBuilder.BuildBody(req);
            Console.WriteLine(body);
        }
    }
}

