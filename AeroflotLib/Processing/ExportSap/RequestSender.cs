using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using ABBYY.FlexiCapture;
using FC12;
using NLog;

namespace AeroflotLib.Processing.ExportSap
{
    public class RequestSender
    {
        private readonly IDocument _document;
        private readonly ILogger _logger;
        private readonly string ExportedPathParameter = "ExportedFilePath"; 
        public RequestSender(IDocument document, IProcessingCallback processing)
        {
            _document = document;
            _logger = new FC12Logger(document, processing).logger;
        }



        public XmlDocument CreateRequestBody()
        {
            throw new NotImplementedException();
        }

        

        private string GetExportedFilePath()
        {
            try {return  _document.Properties.Get(ExportedPathParameter);}
            catch 
            {
                string errorText = $"Document registration parameter {ExportedPathParameter} not found";
                _logger.Error(errorText);
                throw new Exception(errorText);
            }
        }



    }
}
