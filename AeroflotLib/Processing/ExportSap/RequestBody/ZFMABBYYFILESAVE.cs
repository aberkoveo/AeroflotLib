using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace AeroflotLib.Processing.ExportSap.RequestBody
{
    [XmlRoot(ElementName = "ZFM_ABBYY_FILE_SAVE")]
    public class ZFMABBYYFILESAVE
    {

        [XmlElement(ElementName = "I_CONTENT", Namespace = "")]
        public string ICONTENT { get; set; }

        [XmlElement(ElementName = "I_DOCTYPE", Namespace = "")]
        public string IDOCTYPE { get; set; }

        [XmlElement(ElementName = "I_OBJKEY", Namespace = "")]
        public string IOBJKEY { get; set; }

        [XmlElement(ElementName = "I_OBJTYPE", Namespace = "")]
        public string IOBJTYPE { get; set; }
    }
    [XmlRoot(ElementName = "Body")]
    public class Body
    {

        [XmlElement(ElementName = "ZFM_ABBYY_FILE_SAVE", Namespace = "urn:sap-com:document:sap:rfc:functions")]
        public ZFMABBYYFILESAVE ZFMABBYYFILESAVE { get; set; }
    }

    [XmlRoot(ElementName = "Envelope", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
    public class Envelope
    {

        [XmlNamespaceDeclarations]
        public XmlSerializerNamespaces Namespaces { get; set; } = new XmlSerializerNamespaces();

        [XmlElement(ElementName = "Header", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
        public object Header { get; set; }

        [XmlElement(ElementName = "Body",Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
        public Body Body { get; set; }

        [XmlAttribute(AttributeName = "soapenv")]
        public string Soapenv { get; set; }

        [XmlAttribute(AttributeName = "urn")]
        public string Urn { get; set; }

        [XmlText]
        public string Text { get; set; }
    }
}