using System;
using System.IO;
using System.Net;
using System.Xml;

namespace zfmabbyy
{
    public class Send_File
    {
        static void Main(string[] args)
        {
            Send_File obj = new Send_File();
            string ans;

            string[] req = new string[4];
            req[0] = "JVBERi0xLjUKJeLjz9MKOSAwIG9iago8PC9UeXBlL1hPYmplY3QvU3VidHlwZS9Gb3JtL0JCb3hbMCAwIDYwOS4xIDg0NC4zXS9SZXNvdXJjZXMgMTAgMCBSL0ZpbHRl";    //I_CONTENT
            req[1] = "3";    //I_DOCTYPE optional
            req[2] = " 2020";    //I_OBJKEY
            req[3] = "BUS2081";    //I_OBJTYPE

            ans = obj.InvokeService(req);
            System.Console.WriteLine(ans);
            System.Console.ReadLine();
        }
        public string InvokeService(string[] req)
        {
            //Calling CreateSOAPWebRequest method  
            HttpWebRequest request = CreateSOAPWebRequest();

            XmlDocument SOAPReqBody = new XmlDocument();
            //SOAP Body Request  
            SOAPReqBody.LoadXml(@"<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:urn=""urn:sap-com:document:sap:rfc:functions"">
   <soapenv:Header/>
   <soapenv:Body>
      <urn:ZFM_ABBYY_FILE_SAVE><I_CONTENT>" + req[0] + @"</I_CONTENT>
         <I_DOCTYPE>" + req[1] + @"</I_DOCTYPE>
         <I_OBJKEY>" + req[2] + @"</I_OBJKEY>
         <I_OBJTYPE>" + req[3] + @"</I_OBJTYPE>
      </urn:ZFM_ABBYY_FILE_SAVE>
   </soapenv:Body>
</soapenv:Envelope>");

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

                        XmlDocument xDoc = new XmlDocument();
                        xDoc.LoadXml(ServiceResult);

                        XmlNode xNode = xDoc.SelectSingleNode("//E_RESULT");

                        return xNode.InnerText;
                    }
                }
            }
            catch (System.Net.WebException we)
            {
                System.Console.WriteLine(we.Message + "\n" + we.InnerException);
                Console.ReadLine();
                return "ERROR";
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message + "\n" + e.InnerException);
                Console.ReadLine();
                return "ERROR";
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
            HttpWebRequest Req = (HttpWebRequest)WebRequest.Create(@"http://erp-ci.msk.aeroflot.ru:8002/sap/bc/srt/rfc/sap/zfm_abbyy_file_save/300/zfm_abbyy_file_save/zfm_abbyy_file_save?MessageId=" + messageID);
            //SOAPAction  
            Req.Headers.Add(@"SOAPAction:urn:sap-com:document:sap:rfc:functions:ZFM_ABBYY_FILE_SAVE:ZFM_ABBYY_FILE_SAVERequest");
            //Content_type  
            Req.ContentType = "text/xml;charset=\"utf-8\"";
            Req.Accept = "text/xml";
            //HTTP method  
            Req.Method = "POST";

            string login = "aleremote";
            string password = "q1234567";
            Req.Headers.Add("Authorization", "Basic " + Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(login+":"+password)));
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