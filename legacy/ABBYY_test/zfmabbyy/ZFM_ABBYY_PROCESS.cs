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

            //Console.WriteLine(Tools.formatDate("2019/09/05"));
            //Console.ReadKey();

            /*StringWriter sw = new StringWriter();
            XmlTextWriter xtw = new XmlTextWriter(sw);

            int n = 1;  //кол-во items в FL_SERV_ITEMS
            int m = 1;  //кол-во items в ITEMS

            xtw.WriteStartElement("soapenv:Envelope");
            xtw.WriteAttributeString("xmlns", "soapenv", null, "http://schemas.xmlsoap.org/soap/envelope/");
            xtw.WriteAttributeString("xmlns", "urn", null, "urn:sap-com:document:sap:rfc:functions");
            xtw.WriteStartElement("Header", "http://schemas.xmlsoap.org/soap/envelope/");
            xtw.WriteEndElement();  //close Header
            xtw.WriteStartElement("Body", "http://schemas.xmlsoap.org/soap/envelope/");
            xtw.WriteStartElement("ZFM_ABBYY_PROCESS", "urn:sap-com:document:sap:rfc:functions");
            xtw.WriteStartElement("IS_DATA");
            xtw.WriteElementString("PROC_TYPE", "90");
            xtw.WriteStartElement("VEND_CODE_IN");
            xtw.WriteElementString("INN", "1660000344");
            xtw.WriteElementString("KPP", "");
            xtw.WriteElementString("BIC_SWIFT", "");
            xtw.WriteElementString("BANKN", "");
            xtw.WriteElementString("IBAN", "");
            xtw.WriteElementString("NAME", "");
            xtw.WriteElementString("PARTNER", "");
            xtw.WriteElementString("ACCOUNT_ID", "");
            xtw.WriteElementString("EXT_AGR_NUM", "");
            xtw.WriteElementString("KDOKNR", "");
            xtw.WriteEndElement();  //close VEND_CODE_IN
            xtw.WriteStartElement("HEADER_DATA");
            xtw.WriteElementString("PACK", "");
            xtw.WriteElementString("ID", "");
            xtw.WriteElementString("PROCESS_TYPE", "");
            xtw.WriteElementString("DOCNUM", "");
            xtw.WriteElementString("DOCDATE", "");
            xtw.WriteElementString("CRED_NAME", "");
            xtw.WriteElementString("CRED_ADDR", "");
            xtw.WriteElementString("CRED_INN", "");
            xtw.WriteElementString("CRED_KPP", "");
            xtw.WriteElementString("CRED_SWIFT", "");
            xtw.WriteElementString("CRED_IBAN", "");
            xtw.WriteElementString("CRED_CORR", "");
            xtw.WriteElementString("CRED_BANK", "");
            xtw.WriteElementString("CUST_NAME", "");
            xtw.WriteElementString("CUST_ADDR", "");
            xtw.WriteElementString("CUST_INN", "");
            xtw.WriteElementString("CUST_KPP", "");
            xtw.WriteElementString("LINK_DOC", "");
            xtw.WriteElementString("CONTRACT", "");
            xtw.WriteElementString("CONSIGNOR", "");
            xtw.WriteElementString("CONSIGNEE", "");
            xtw.WriteElementString("DUE_DATE", "");
            xtw.WriteElementString("TOTAL", "");
            xtw.WriteElementString("VAT", "");
            xtw.WriteElementString("TOTAL_INCL_VAT", "");
            xtw.WriteElementString("CURRENCY", "");
            xtw.WriteElementString("SOURCE_DOC", "");
            xtw.WriteElementString("ZZ_SERV_DATE_FROM", "");
            xtw.WriteElementString("ZZ_SERV_DATE_TO", "");
            xtw.WriteElementString("SCAN_STATION", "");
            xtw.WriteElementString("SCAN_NAME", "");
            xtw.WriteElementString("SCAN_DATE", "");
            xtw.WriteElementString("SCAN_TIME", "");
            xtw.WriteElementString("CONTR_NAME", "");
            xtw.WriteElementString("LIFNR", "");
            xtw.WriteElementString("ZUONR", "");
            xtw.WriteEndElement();  //close HEADER_DATA
            xtw.WriteStartElement("ITEM_DATA");
            for (int i = 0; i < n; i++)
            {
                xtw.WriteStartElement("item");
                xtw.WriteElementString("PACK", "");
                xtw.WriteElementString("ID", "");
                xtw.WriteElementString("SERV_DATE", "");
                xtw.WriteElementString("SERV_TIME", "");
                xtw.WriteElementString("FLIGHT_NUM", "");
                xtw.WriteElementString("TAIL_NUM", "");
                xtw.WriteElementString("AC_TYPE", "");
                xtw.WriteStartElement("FLIGHT_SERV_ITEMS");
                xtw.WriteStartElement("item");
                for (int j = 0; j < m; j++)
                {
                    xtw.WriteStartElement("item");
                    xtw.WriteElementString("PACK", "");
                    xtw.WriteElementString("ID", "");
                    xtw.WriteElementString("DESCRIPTION", "");
                    xtw.WriteElementString("COEFF", "");
                    xtw.WriteElementString("UOM", "");
                    xtw.WriteElementString("QUANTITY", "");
                    xtw.WriteElementString("PRICE", "");
                    xtw.WriteElementString("DISCOUNT", "");
                    xtw.WriteElementString("TOTAL_WO_VAT", "");
                    xtw.WriteElementString("VAT", "");
                    xtw.WriteElementString("TOTAL_W_VAT", "");
                    xtw.WriteElementString("NOTE", "");
                    xtw.WriteEndElement();  //close item
                }
                xtw.WriteEndElement();  //close item
                xtw.WriteEndElement();  //close FLIGHT_SERV_ITEMS
                xtw.WriteEndElement();  //close item
            }
            xtw.WriteEndElement();  //close FL_SERV_ITEMS
            xtw.WriteEndElement();  //close IS_DATA
            xtw.WriteEndElement();  //close ZFM_ABBYY_PROCESS
            xtw.WriteEndElement();  //close Body
            xtw.WriteEndElement();  //close Envelope

            

            string s = sw.ToString();
            xtw.Close();
            sw.Close();*/
            //Console.Out.WriteLine(s);
            //Console.ReadLine();
            
            string s1 = "<soapenv:Envelope xmlns:soapenv=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:urn=\"urn:sap-com:document:sap:rfc:functions\"><soapenv:Header /><soapenv:Body><urn:ZFM_ABBYY_PROCESS><IS_DATA><PROC_TYPE>90</PROC_TYPE><VEND_CODE_IN><INN>1660000344</INN><KPP>162401001</KPP><NAME>АО «Международный аэропорт «Казань»</NAME><BIC_SWIFT /><BANKN /></VEND_CODE_IN></IS_DATA></urn:ZFM_ABBYY_PROCESS></soapenv:Body></soapenv:Envelope>";

            XmlDocument xDoc = new XmlDocument();
            xDoc = obj.InvokeService(s1);

            return;

           /* xDoc.SelectSingleNode("//BELNR");
            xDoc.SelectSingleNode("//GJAHR");
            xDoc.SelectSingleNode("//INN");
            xDoc.SelectSingleNode("//KPP");
            xDoc.SelectSingleNode("//BIC_SWIFT");
            xDoc.SelectSingleNode("//BANKN");
            xDoc.SelectSingleNode("//IBAN");
            xDoc.SelectSingleNode("//NAME");
            s1=xDoc.SelectSingleNode("//LIFNR").InnerText;
            xDoc.SelectSingleNode("//AGR_NUM");
            xDoc.SelectSingleNode("//FLAG");
            //System.Console.WriteLine(xDoc.SelectSingleNode("//BELNR").InnerText);


            //Console.WriteLine("s1:"+s1);
            Console.ReadLine();*/

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

                        //XmlNode xNode = xDoc.SelectSingleNode("//E_PARTNER");
                        //System.Console.WriteLine(xNode);
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
            //System.Console.WriteLine(s);
            //Making Web Request  
            HttpWebRequest Req = (HttpWebRequest)WebRequest.Create(@"http://erp-ci.msk.aeroflot.ru:8002/sap/bc/srt/rfc/sap/zfm_abbyy_process/300/zfm_abbyy_process/zfm_abbyy_process?MessageId=" + messageID);
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