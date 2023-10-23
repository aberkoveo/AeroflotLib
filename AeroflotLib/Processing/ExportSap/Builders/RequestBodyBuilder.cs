using AeroflotLib.Processing.ExportSap.RequestBody;
using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Xml;

namespace AeroflotLib.Processing.ExportSap.Builders
{
    public static class RequestBodyBuilder
    {
        public static XmlDocument BuildBody(string[] parameters)
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

            Envelope xml = BuildEnvelope(parameters);
            serializer.Serialize(xmlWriter, xml, myNamespaces);

            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(stringwriter.ToString());

            return xmlDocument;
        }

        private static Envelope BuildEnvelope(string[] parameters)
        {
            return new Envelope()
            {
                Body = new Body()
                {
                    ZFMABBYYFILESAVE = new ZFMABBYYFILESAVE()
                    {
                        ICONTENT = parameters[0],
                        IDOCTYPE = parameters[1],
                        IOBJKEY = parameters[2],
                        IOBJTYPE = parameters[3]
                    }
                }
            };
        }
    }
}
