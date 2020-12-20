using ErsSapAPI.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ErsSapAPI.Services
{
    public class HotPoService : IHotPoService
    {
        public string Test(string s)
        {
            Console.WriteLine("Test Method Executed!");
            return s;
        }

        public HotPurchaseOrder TestCustomModel(HotPurchaseOrder inputModel)
        {
            return inputModel;
        }

        public void XmlMethod(XElement xml)
        {
            Console.WriteLine(xml.ToString());
        }
        public async static Task<string> CallWebService(string webWebServiceUrl,
                               string webServiceNamespace,
                               string methodVerb,
                               string methodName,
                               Dictionary<string, string> parameters)
        {
            const string soapTemplate =
            @"<soap:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/""
                        xmlns:{0}=""{2}"">
            <soap:Header/>
   <soap:Body>
      <urn:ZrtFm036>
         <IProcess>I</IProcess>
         <IsHeader>
            <PoNumber></PoNumber>
            <DocType>NB</DocType>
            <Vendor>0000010139</Vendor>
            <PurchOrg>ir51</PurchOrg>
            <PurGroup>101</PurGroup>
            <CompCode>IR50</CompCode>
            <Pmnttrms></Pmnttrms>
            <Incoterms1></Incoterms1>
            <Incoterms2></Incoterms2>
            <DocDate>2020-12-19</DocDate>
         </IsHeader>
         <ItItem>
            <!--Zero or more repetitions:-->
            <item>
               <PoItem></PoItem>
               <Acctasscat></Acctasscat>
               <ItemCat></ItemCat>
               <Material>000000000001000348</Material>
               <ShortText></ShortText>
               <Plant>1010</Plant>
               <StgeLoc>0001</StgeLoc>
               <MatlGroup></MatlGroup>
               <Quantity>10</Quantity>
               <PoUnit>ST</PoUnit>
               <NetPrice>1</NetPrice>
               <PriceUnit>1</PriceUnit>
               <TaxCode></TaxCode>
               <NoMoreGr></NoMoreGr>
               <FreeItem></FreeItem>
               <Agreement></Agreement>
               <AgmtItem></AgmtItem>
               <ConfCtrl></ConfCtrl>
               <PreqNo></PreqNo>
               <PreqItem></PreqItem>
               <RetItem></RetItem>
               <OrderReason></OrderReason>
               <VendPart></VendPart>
               <GlAccount></GlAccount>
               <BusArea></BusArea>
               <Costcenter></Costcenter>
               <AssetNo></AssetNo>
               <SubNumber></SubNumber>
               <WbsElement></WbsElement>
            </item>
         </ItItem>
      </urn:ZrtFm036>
   </soap:Body>
</soap:Envelope>";

            var req = (HttpWebRequest)WebRequest.Create(webWebServiceUrl);
            req.ContentType = "text/xml"; //"application/soap+xml;";
            req.Method = "POST";

            string parametersText;

            if (parameters != null && parameters.Count > 0)
            {
                var sb = new StringBuilder();
                foreach (var oneParameter in parameters)
                    sb.AppendFormat("  <{0}>{1}</{0}>\r\n", oneParameter.Key, oneParameter.Value);

                parametersText = sb.ToString();
            }
            else
            {
                parametersText = "";
            }

            string soapText = string.Format(soapTemplate,
                            methodVerb, methodName, webServiceNamespace, parametersText);

            Console.WriteLine("SOAP call to: {0}", webWebServiceUrl);
            Console.WriteLine(soapText);

            using (Stream stm = await req.GetRequestStreamAsync())
            {
                using (var stmw = new StreamWriter(stm))
                {
                    stmw.Write(soapText);
                }
            }

            var responseHttpStatusCode = HttpStatusCode.Unused;
            string responseText = null;

            using (var response = (HttpWebResponse)req.GetResponseAsync().Result)
            {
                responseHttpStatusCode = response.StatusCode;

                if (responseHttpStatusCode == HttpStatusCode.OK)
                {
                    int contentLength = (int)response.ContentLength;

                    if (contentLength > 0)
                    {
                        int readBytes = 0;
                        int bytesToRead = contentLength;
                        byte[] resultBytes = new byte[contentLength];

                        using (var responseStream = response.GetResponseStream())
                        {
                            while (bytesToRead > 0)
                            {
                                // Read may return anything from 0 to 10. 
                                int actualBytesRead = responseStream.Read(resultBytes, readBytes, bytesToRead);

                                // The end of the file is reached. 
                                if (actualBytesRead == 0)
                                    break;

                                readBytes += actualBytesRead;
                                bytesToRead -= actualBytesRead;
                            }

                            responseText = Encoding.UTF8.GetString(resultBytes);
                            //responseText = Encoding.ASCII.GetString(resultBytes);
                        }
                    }
                }
            }
            return responseText;
            //return responseHttpStatusCode;
        }
    }
}
