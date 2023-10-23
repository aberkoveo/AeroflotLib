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

namespace AeroflotLib.Processing.ExportSap
{
    public class SapExportModule
    {
        private readonly IDocument _document;
        private readonly ILogger _logger;

        public SapExportModule(IDocument document, IProcessingCallback processing)
        {
            _document = document;
            _logger = new FC12Logger(document, processing).logger;
        }

        public void ExportDocument(string[] bodyParameters)
        {
            SapRequestParameters parameters = SapRequestParametersBuilder.BuildSapRequestParameters(_document);
            XmlDocument requestBody = RequestBodyBuilder.BuildBody(bodyParameters);
            HttpWebRequest request = RequestBuilder.CreateSapRequest(parameters);
            
            try
            {
                using (Stream stream = request.GetRequestStream())
                {
                    requestBody.Save(stream);
                }

                using (WebResponse response = request.GetResponse())
                {
                    using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                    {
                        var ServiceAnswer = reader.ReadToEnd();

                        XmlDocument xmlDocument = new XmlDocument();
                        xmlDocument.LoadXml(ServiceAnswer);
                        XmlNode xmlNode = xmlDocument.SelectSingleNode("//E_RESULT");


                        _logger.Info(xmlNode.InnerText);
                    }
                }
            }
            catch (WebException webException)
            {
                _logger.Error(webException.Message + "\n" + webException.InnerException);
            }
            catch (Exception exception)
            {
                _logger.Error(exception.Message + "\n" + exception.InnerException);
            }


        }
    }
}
