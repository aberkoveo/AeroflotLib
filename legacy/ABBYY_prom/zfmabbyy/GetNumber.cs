using System;
using System.IO;
using System.Net;
using System.Xml;

namespace zfmabbyy
{
    public class GetNumber
    {
        static void Main(string[] args)
        {


            GetNumber obj = new GetNumber();
            string ans;
            //ans=obj.InvokeService("MainTest");
            //System.Console.WriteLine(ans);
            //System.Console.ReadLine();
        }
        public string[] InvokeService(string[] req)
        {
            //Calling CreateSOAPWebRequest method  
            HttpWebRequest request = CreateSOAPWebRequest();

            XmlDocument SOAPReqBody = new XmlDocument();
            //SOAP Body Request  
            SOAPReqBody.LoadXml(@"<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:urn=""urn:sap-com:document:sap:rfc:functions"">
   <soapenv:Header/>
   <soapenv:Body>
      <urn:ZKZU_GET_PARTNER_NUMBER>
         <I_BANKN>" + req[0] + @"</I_BANKN>
         <I_BIC_SWIFT>" + req[1] + @"</I_BIC_SWIFT>
         <I_DATE>" + req[2] + @"</I_DATE>
         <I_IBAN>" + req[3] + @"</I_IBAN>
         <I_INN>" + req[4] + @"</I_INN>
         <I_KPP>" + req[5] + @"</I_KPP>
         <I_LIFNR>" + req[6] + @"</I_LIFNR>
         <I_NAME>" + req[7] + @"</I_NAME>
         <I_NOTE>" + req[8] + @"</I_NOTE>
         <I_NUM_DOC>" + req[9] + @"</I_NUM_DOC>
         <I_TOTAL>" + req[10] + @"</I_TOTAL>
         <I_TYPE>" + req[11] + @"</I_TYPE>
         <I_VAT>" + req[12] + @"</I_VAT>
         <I_VAT_TOTAL>" + req[13] + @"</I_VAT_TOTAL>
         <I_WAERS>" + req[14] + @"</I_WAERS>
      </urn:ZKZU_GET_PARTNER_NUMBER>
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
                        //writting stream result on console  
                        //Console.WriteLine(ServiceResult);
                        //Console.ReadLine();

                        XmlDocument xDoc = new XmlDocument();
                        xDoc.LoadXml(ServiceResult);

                        //XmlNode xNode = xDoc.SelectSingleNode("//E_PARTNER");
                        //System.Console.WriteLine(xNode.InnerText);
                        

                        return new string[] { xDoc.SelectSingleNode("//E_PARTNER").InnerText, xDoc.SelectSingleNode("//E_BELNR").InnerText, xDoc.SelectSingleNode("//E_GJAHR").InnerText };
                    }
                }
            }
            catch (System.Net.WebException we)
            {
                System.Console.WriteLine(we.Message + "\n" + we.InnerException);
                Console.ReadLine();
                return new string[] { "ERROR" };
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message + "\n" + e.InnerException);
                Console.ReadLine();
                return new string[] { "ERROR" };
            }
        }

        public HttpWebRequest CreateSOAPWebRequest()
        {

            //Generate random id message
            Random rnd = new Random();
            string s = "";
            for (int j = 0; j < 36; j++)
            {
                if (j == 8 || j == 13 || j == 18 || j == 23)
                {
                    s += "-";
                }
                else
                {
                    s += getRand(rnd);
                }
            }
            //System.Console.WriteLine(s);
            //Making Web Request  
            HttpWebRequest Req = (HttpWebRequest)WebRequest.Create(@"http://erp-pp1.msk.aeroflot.ru:8052/sap/bc/srt/rfc/sap/zkzu_get_partner_number/300/zkzu_get_partner_number/zkzu_get_partner_number?MessageId="+s);
            //SOAPAction  
            Req.Headers.Add(@"SOAPAction:urn:sap-com:document:sap:rfc:functions:ZKZU_GET_PARTNER_NUMBER:ZKZU_GET_PARTNER_NUMBERRequest");
            //Content_type  
            Req.ContentType = "text/xml;charset=\"utf-8\"";
            Req.Accept = "text/xml";
            //HTTP method  
            Req.Method = "POST";

            Req.Headers.Add("Authorization", "Basic QUJCWVk6MTIzNDU2Nzg=");
            //return HttpWebRequest

            return Req;
        }
        public char getRand(Random rnd)
        {
            if (rnd.Next(2) == 1)
            {
                return Char.Parse(new Random().Next(10).ToString());
            }
            else
            {
                return (char)(rnd.Next(26) + 65);
            }
        }
    }
}