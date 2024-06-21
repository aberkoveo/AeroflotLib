using System;
using System.IO;
using System.Net;
using System.Xml;

namespace zfmabbyy
{
    public class ZFM_ABBYY_PROCESS
    {
        static void Main(string[] args)
        {
            ZFM_ABBYY_PROCESS obj = new ZFM_ABBYY_PROCESS();

            
            string s1 = "<soapenv:Envelope xmlns:soapenv=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:urn=\"urn:sap-com:document:sap:rfc:functions\"><soapenv:Header /><soapenv:Body><urn:ZFM_ABBYY_PROCESS><IS_DATA><PROC_TYPE>90</PROC_TYPE><VEND_CODE_IN><INN>1660000344</INN><KPP>162401001</KPP><NAME>АО «Международный аэропорт «Казань»</NAME><BIC_SWIFT /><BANKN /></VEND_CODE_IN></IS_DATA></urn:ZFM_ABBYY_PROCESS></soapenv:Body></soapenv:Envelope>";

            XmlDocument xDoc = new XmlDocument();
            xDoc = obj.InvokeService(s1);

            return;


        }
        public XmlDocument InvokeService(string s)
        {
            //Calling CreateSOAPWebRequest method  
            HttpWebRequest request = CreateSOAPWebRequest();

            XmlDocument SOAPReqBody = new XmlDocument();
            //SOAP Body Request  
            SOAPReqBody.LoadXml(s);

            return null;

            try
            {
                using (Stream stream = request.GetRequestStream())
                {
                    SOAPReqBody.Save(stream);
                }
                //Geting response from request  
                using (WebResponse Serviceres = request.GetResponse())
                {
                    using (StreamReader rd = new StreamReader(Serviceres.GetResponseStream()))
                    {
                        //reading stream  
                        var ServiceResult = rd.ReadToEnd();
                        //writting stream result on console  

                        XmlDocument xDoc = new XmlDocument();
                        xDoc.LoadXml(ServiceResult);
                        return xDoc;

                    }
                }
            }
            catch (System.Net.WebException we)
            {
                XmlDocument xDoc = new XmlDocument();
                xDoc.LoadXml("<root><WebException>"+we.Message + "  " + we.InnerException+"</WebException></root>");
                return xDoc;
            }
            catch (Exception e)
            {
                XmlDocument xDoc = new XmlDocument();
                xDoc.LoadXml("<root><WebException>" + e.Message + "  " + e.InnerException + "</WebException></root>");
                return xDoc;
            }
        }

        public HttpWebRequest CreateSOAPWebRequest()
        {

            //Generate random id message
            Random rnd = new Random();
            string messageID = "";
            for (int j = 0; j < 36; j++)
            {
                if (j == 8 || j == 13 || j == 18 || j == 23)
                {
                    messageID += "-";
                }
                else
                {
                    messageID += getRand(rnd);
                }
            }
            //Making Web Request  
            HttpWebRequest Req = (HttpWebRequest)WebRequest.Create(@"http://172.16.41.176:8052/sap/bc/srt/rfc/sap/zfm_abbyy_process/300/zfm_abbyy_process/zfm_abbyy_process?MessageId=" + messageID);
            //SOAPAction  
            Req.Headers.Add(@"urn:sap-com:document:sap:rfc:functions:ZFM_ABBYY_PROCESS:ZFM_ABBYY_PROCESSRequest");
            //Content_type  
            Req.ContentType = "text/xml;charset=\"utf-8\"";
            Req.Accept = "text/xml";
            //HTTP method  
            Req.Method = "POST";
            Req.Timeout = 600000;

            string login = "aleremote";
            string password = "q1234567";
            Req.Headers.Add("Authorization", "Basic " + Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(login + ":" + password)));
            //return HttpWebRequest

            return Req;
        }
        public char getRand(Random rnd)
        {
            if (rnd.Next(2) == 1)
            {
                return Char.Parse(rnd.Next(10).ToString());
            }
            else
            {
                return (char)(rnd.Next(26) + 65);
            }
        }
    }
}