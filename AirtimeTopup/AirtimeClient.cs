﻿using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using AirtimeTopup.Models;
using Newtonsoft.Json;

namespace AirtimeTopup
{
    public class AirtimeClient
    {
        private String URL = String.Empty;
        private String ClientId = String.Empty;
        private String ClientKey = String.Empty;
        private String TerminalId = "AirtimeClient";

        public AirtimeClient()
        {
            this.URL = ConfigurationManager.AppSettings["URL"];
            this.ClientId = ConfigurationManager.AppSettings["ClientId"];
            this.ClientKey = ConfigurationManager.AppSettings["ClientKey"];
        }

        public AirtimeClient(String URL, String ClientId, String ClientKey)
        {
            this.URL = URL;
            this.ClientId = ClientId;
            this.ClientKey = ClientKey;
        }

        public AirtimeClient(String ClientId, String ClientKey)
        {
            this.URL = ConfigurationManager.AppSettings["URL"];
            this.ClientId = ClientId;
            this.ClientKey = ClientKey;
        }

        public static Network Parse(string phoneNumber)
        {
            switch (phoneNumber.Substring(0,3))
            {
                case "016":
                    return Network.AIR;

                case "017":
                    return Network.GP;

                case "019":
                    return Network.BL;

                case "018":
                    return Network.ROBI;
                //case "AIR":
                //case "AIRTEL":
                //    return Network.AIR;
                //case "GP":
                //case "GrameenPhone":
                //    return Network.GP;
                //case "BL":
                //case "Banglalink":
                //    return Network.BL;
                //case "ROBI":
                //    return Network.ROBI;

                default:
                    throw new Exception("Unknown Network");
            }
        }

        public BalanceResult Balance()
        {
            String Nonce = DateTime.Now.ToString("yyyyMMddHHmmssfff");

            WebClient http = new WebClient();
            String Signature = this.ComputeSignature(Nonce, http.QueryString);

            http.Headers.Add("ClientId", this.ClientId);
            http.Headers.Add("Signature", Signature);
            http.Headers.Add("Nonce", Nonce);

            String response = http.DownloadString(this.URL + "/airtime/Balance");
            BalanceResult result = JsonConvert.DeserializeObject<BalanceResult>(response);
            return result;
        }

        public BalanceResult AgentBalance()
        {
            String Nonce = DateTime.Now.ToString("yyyyMMddHHmmssfff");

            WebClient http = new WebClient();
            String Signature = this.ComputeSignature(Nonce, http.QueryString);

            http.Headers.Add("AgentId", this.ClientId);
            http.Headers.Add("Signature", Signature);
            http.Headers.Add("Nonce", Nonce);
            http.Headers.Add("TerminalId", this.TerminalId);

            String response = http.DownloadString(this.URL + "/terminal/Balance");
            BalanceResult result = JsonConvert.DeserializeObject<BalanceResult>(response);
            return result;
        }

        public AirtimeResult Credit(Network net, String msisdn, int amount, string xref)
        {
            AirtimeResult result = new AirtimeResult();
            result.ResultCode = 0;
            String Nonce = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            msisdn = Regex.Replace(msisdn, @"(?<PREFIX>^0)(?<TAIL>\d*)", @"234${TAIL}");

            try
            {
                WebClient http = new WebClient();
                http.QueryString.Add("net", net.ToString());
                http.QueryString.Add("msisdn", msisdn);
                http.QueryString.Add("amount", amount.ToString());
                http.QueryString.Add("xref", xref);

                String Signature = this.ComputeSignature(Nonce, http.QueryString);               

                http.Headers.Add("ClientId", this.ClientId);
                http.Headers.Add("Signature", Signature);
                http.Headers.Add("Nonce", Nonce);

                String response = http.DownloadString(this.URL + "/airtime/Credit");
                result = JsonConvert.DeserializeObject<AirtimeResult>(response);
                result.ResultCode = 200;
            }
            catch (WebException wex)
            {
                if (wex.Status == WebExceptionStatus.ProtocolError)
                {
                    HttpStatusCode httpStatus = ((HttpWebResponse)wex.Response).StatusCode;
                    result.ResultCode = (int) httpStatus;
                    result.message = wex.Message;
                    result.xref = xref;
                }
            }

            return result;
        }

        public AirtimeResult DataCredit(Network net, String msisdn, int amount, string xref)
        {
            AirtimeResult result = new AirtimeResult();
            result.ResultCode = 0;
            String Nonce = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            msisdn = Regex.Replace(msisdn, @"(?<PREFIX>^0)(?<TAIL>\d*)", @"234${TAIL}");

            try
            {
                WebClient http = new WebClient();
                http.QueryString.Add("net", net.ToString());
                http.QueryString.Add("msisdn", msisdn);
                http.QueryString.Add("amount", amount.ToString());
                http.QueryString.Add("xref", xref);

                String Signature = this.ComputeSignature(Nonce, http.QueryString);               

                http.Headers.Add("ClientId", this.ClientId);
                http.Headers.Add("Signature", Signature);
                http.Headers.Add("Nonce", Nonce);

                String response = http.DownloadString(this.URL + "/data/Credit");
                result = JsonConvert.DeserializeObject<AirtimeResult>(response);
                result.ResultCode = 200;
            }
            catch (WebException wex)
            {
                if (wex.Status == WebExceptionStatus.ProtocolError)
                {
                    HttpStatusCode httpStatus = ((HttpWebResponse)wex.Response).StatusCode;
                    result.ResultCode = (int) httpStatus;
                    result.message = wex.Message;
                    result.xref = xref;
                }
            }

            return result;
        }

        public AirtimeResult AgentCredit(Network net, String msisdn, int amount, string xref)
        {
            AirtimeResult result = new AirtimeResult();
            result.ResultCode = 0;
            String Nonce = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            msisdn = Regex.Replace(msisdn, @"(?<PREFIX>^0)(?<TAIL>\d*)", @"234${TAIL}");

            try
            {
                WebClient http = new WebClient();
                http.QueryString.Add("net", net.ToString());
                http.QueryString.Add("msisdn", msisdn);
                http.QueryString.Add("amount", amount.ToString());
                http.QueryString.Add("xref", xref);

                String Signature = this.ComputeSignature(Nonce, http.QueryString);                

                http.Headers.Add("AgentId", this.ClientId);
                http.Headers.Add("Signature", Signature);
                http.Headers.Add("Nonce", Nonce);
                http.Headers.Add("TerminalId", this.TerminalId);

                String response = http.DownloadString(this.URL + "/terminal/Credit");
                result = JsonConvert.DeserializeObject<AirtimeResult>(response);
                result.ResultCode = 200;
            }
            catch (WebException wex)
            {
                if (wex.Status == WebExceptionStatus.ProtocolError)
                {
                    result.ResultCode = (int) ((HttpWebResponse)wex.Response).StatusCode;
                }
            }

            return result;
        }

        public AirtimeResult AgentDataCredit(Network net, String msisdn, int amount, string xref)
        {
            AirtimeResult result = new AirtimeResult();
            result.ResultCode = 0;
            String Nonce = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            msisdn = Regex.Replace(msisdn, @"(?<PREFIX>^0)(?<TAIL>\d*)", @"234${TAIL}");

            try
            {
                WebClient http = new WebClient();
                http.QueryString.Add("net", net.ToString());
                http.QueryString.Add("msisdn", msisdn);
                http.QueryString.Add("amount", amount.ToString());
                http.QueryString.Add("xref", xref);

                String Signature = this.ComputeSignature(Nonce, http.QueryString);                

                http.Headers.Add("AgentId", this.ClientId);
                http.Headers.Add("Signature", Signature);
                http.Headers.Add("Nonce", Nonce);
                http.Headers.Add("TerminalId", this.TerminalId);

                String response = http.DownloadString(this.URL + "/terminal/data/Credit");
                result = JsonConvert.DeserializeObject<AirtimeResult>(response);
                result.ResultCode = 200;
            }
            catch (WebException wex)
            {
                if (wex.Status == WebExceptionStatus.ProtocolError)
                {
                    result.ResultCode = (int) ((HttpWebResponse)wex.Response).StatusCode;
                }
            }

            return result;
        }

        public CheckResult Check(String reference)
        {
            String Nonce = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            String Xref = Guid.NewGuid().ToString("N");

            WebClient http = new WebClient();
            http.QueryString.Add("reference", reference);

            String Signature = this.ComputeSignature(Nonce, http.QueryString);

            http.Headers.Add("ClientId", this.ClientId);
            http.Headers.Add("Signature", Signature);
            http.Headers.Add("Nonce", Nonce);

            String response = http.DownloadString(this.URL + "/airtime/Check");
            CheckResult result = JsonConvert.DeserializeObject<CheckResult>(response);
            return result;
        }

        public AllocateSingleResult AllocateSingle(int unit, String type, String message, String xref)
        {
            String Nonce = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            String Xref = Guid.NewGuid().ToString("N");

            WebClient http = new WebClient();
            http.QueryString.Add("unit", unit.ToString());
            http.QueryString.Add("type", type);
            http.QueryString.Add("message", message);
            http.QueryString.Add("xref", xref);

            String Signature = this.ComputeSignature(Nonce, http.QueryString);

            http.Headers.Add("ClientId", this.ClientId);
            http.Headers.Add("Signature", Signature);
            http.Headers.Add("Nonce", Nonce);

            String response = http.DownloadString(this.URL + "/pin/AllocateSingle");
            AllocateSingleResult result = JsonConvert.DeserializeObject<AllocateSingleResult>(response);
            return result;
        }

        private string ComputeSignature(String Nonce, NameValueCollection query)
        {
            String signature = String.Empty;
            StringBuilder queryString = new StringBuilder();
            if (query.Count > 0)
            {
                queryString.Append("?");
                queryString.Append(query.GetKey(0));
                queryString.Append("=");
                queryString.Append(query[0]);

                for (int i = 1; i < query.Count; i++)
                {
                    queryString.Append("&");
                    queryString.Append(query.GetKey(i));
                    queryString.Append("=");
                    queryString.Append(query[i]);
                }
            }

            byte[] data = Encoding.UTF8.GetBytes(Nonce + queryString.ToString());
            byte[] secretKeyBytes = Convert.FromBase64String(this.ClientKey);

            using (HMACSHA256 hmac = new HMACSHA256(secretKeyBytes))
            {
                byte[] signatureBytes = hmac.ComputeHash(data);
                signature = Convert.ToBase64String(signatureBytes);
            }

            return signature;
        }
    }
}
