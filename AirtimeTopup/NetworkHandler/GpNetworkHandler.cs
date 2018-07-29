// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GpNetworkHandler.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the GpNetworkHandler type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using AirtimeTopup.Notification;

namespace AirtimeTopup.NetworkHandler
{
    using System;
    using System.Net;

    using AirtimeTopup.Models;

    /// <summary>
    /// The gp network handler.
    /// </summary>
    public class GpNetworkHandler : NetworkHandler
    {
        public GpNetworkHandler()
        {
            this.Url = "https://topup.gp.com:9999";
            this.ClientId = "cefalo";
            this.ClientKey = "password";
            this.BalanceEndpoint = "/airtime/balance";
            this.TopupEndpoint = "/airtime/flexiload";
        }


        public override BalanceResult GetBalance()
        {
            var http = this.CreateWebClientWithHeader();
            var nonce = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            http.Headers.Add("Nonce", nonce);

            var response = this.NetworkResponse(http, this.Url + this.BalanceEndpoint);
            var result = this.NetworkResponseMapToObject<BalanceResult>(response);
            return result;
        }


        public override AirtimeResult TopUp(string phoneNumber, int amount)
        {
            AirtimeResult result = new AirtimeResult();
            result.ResultCode = 0;

            try
            {
                var http = this.CreateWebClientWithHeader();
                var nonce = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                http.Headers.Add("Nonce", nonce);

                http.QueryString.Add("msisdn", phoneNumber);
                http.QueryString.Add("amount", amount.ToString());

                var response = this.NetworkResponse(http, this.Url + this.TopupEndpoint);
                result = this.NetworkResponseMapToObject<AirtimeResult>(response);
                result.ResultCode = 200;

                EventAggregator.Instance.Publish(result);

                this.SaveToDatabase(result);
            }
            catch (WebException wex)
            {
                if (wex.Status == WebExceptionStatus.ProtocolError)
                {
                    HttpStatusCode httpStatus = ((HttpWebResponse)wex.Response).StatusCode;
                    result.ResultCode = (int) httpStatus;
                    result.Message = wex.Message;
                }
            }

            return result;
        }
    }
}
