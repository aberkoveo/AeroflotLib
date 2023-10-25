using ABBYY.FlexiCapture;
using AeroflotLib.Processing.ExportSap.Builders;
using AeroflotLib.Processing.ExportSap.Builders.Parameters;
using AeroflotLib.Processing.ExportSap.Factories.Parameters;
using FC12;
using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace AeroflotLib.Processing.ExportSap
{
    public class SapExportModule
    {
        private readonly IDocument _document;
        private readonly ILogger _logger;
        private readonly IProcessingCallback _processing;

        public SapExportModule(IDocument document, IProcessingCallback processing)
        {
            _document = document;
            _processing  = processing;
            _logger = new FC12Logger(document, processing).logger;
        }

        public void ExportDocument(string[] bodyParameters)
        {
            _processing.ReportMessage("Sap request creation initialized");

            SapRequestParameters parameters = SapRequestParametersBuilder.BuildSapRequestParameters(_document);
            _processing.ReportMessage("Sap request parameters created");

            string requestBody = RequestBodyBuilder.BuildBody(bodyParameters);
            XmlDocument xmlBody = new XmlDocument();
            xmlBody.LoadXml(requestBody);
            _processing.ReportMessage("Sap request body created");

            HttpWebRequest request = RequestBuilder.CreateSapRequest(parameters);
            _processing.ReportMessage("Sap request created");

            try
            {
                using (Stream stream = request.GetRequestStream())
                {
                    xmlBody.Save(stream);
                }

                using (WebResponse response = request.GetResponse())
                {
                    using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                    {
                        var ServiceAnswer = reader.ReadToEnd();

                        XmlDocument xmlDocument = new XmlDocument();
                        xmlDocument.LoadXml(ServiceAnswer);
                        XmlNode xmlNode = xmlDocument.SelectSingleNode("//E_RESULT");

                        _processing.ReportMessage(xmlNode.InnerText);
                        _logger.Info(xmlNode.InnerText);
                    }
                }
            }
            catch (WebException webException)
            {
                _processing.ReportError(webException.Message + "\n" + webException.InnerException);
                _logger.Error(webException.Message + "\n" + webException.InnerException);
                throw new Exception("Отправка вложений в SAP была провалена"); 
            }
            catch (Exception exception)
            {
                _processing.ReportError(exception.Message + "\n" + exception.InnerException);
                _logger.Error(exception.Message + "\n" + exception.InnerException);
                throw new Exception("Отправка вложений в SAP была провалена");
            }


        }
    }
}
