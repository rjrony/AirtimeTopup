// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AirtelNetworkHandler.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the AirtelNetworkHandler type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using AirtimeTopup.Notification;

namespace AirtimeTopup.NetworkHandler
{
    using System.Net;

    using AirtimeTopup.Models;

    /// <summary>
    /// The airtel network handler.
    /// </summary>
    public class AirtelNetworkHandler : NetworkHandler
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AirtelNetworkHandler"/> class.
        /// </summary>
        public AirtelNetworkHandler()
        {
            this.Url = "https://topup.airtel.com:9999";
            this.ClientId = "cefalo";
            this.ClientKey = "password";
            this.BalanceEndpoint = "/airtime/balance";
            this.TopupEndpoint = "/airtime/recharge";
        }
        public override BalanceResult GetBalance()
        {
            var http = this.CreateWebClientWithHeader();

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

                http.QueryString.Add("phone-number", phoneNumber);
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
                    result.ResultCode = (int)httpStatus;
                    result.Message = wex.Message;
                }
            }

            return result;
        }
    }
}
