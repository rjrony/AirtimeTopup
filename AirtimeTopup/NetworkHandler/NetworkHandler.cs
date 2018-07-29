// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NetworkHandler.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the NetworkHandler type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;

namespace AirtimeTopup.NetworkHandler
{
    using System.Net;

    using AirtimeTopup.Models;

    using Newtonsoft.Json;

    /// <summary>
    /// The network handler.
    /// </summary>
    public abstract class NetworkHandler : INetworkHandler
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NetworkHandler"/> class.
        /// </summary>
        public NetworkHandler()
        {
            
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NetworkHandler"/> class.
        /// </summary>
        /// <param name="url">
        /// The url.
        /// </param>
        /// <param name="clientId">
        /// The client id.
        /// </param>
        /// <param name="clientKey">
        /// The client key.
        /// </param>
        public NetworkHandler(string url, string clientId, string clientKey)
        {
            this.Url = url;
            this.ClientId = clientId;
            this.ClientKey = clientKey;
        }

        /// <summary>
        /// Gets or sets the url.
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets the balance endpoint.
        /// </summary>
        public string BalanceEndpoint { get; set; }

        /// <summary>
        /// Gets or sets the topup endpoint.
        /// </summary>
        public string TopupEndpoint { get; set; }

        /// <summary>
        /// Gets or sets the client id.
        /// </summary>
        public string ClientId { get; set; }

        /// <summary>
        /// Gets or sets the client key.
        /// </summary>
        public string ClientKey { get; set; }

        /// <summary>
        /// The get balance.
        /// </summary>
        /// <returns>
        /// The <see cref="BalanceResult"/>.
        /// </returns>
        public abstract BalanceResult GetBalance();

        /// <summary>
        /// The top up.
        /// </summary>
        /// <param name="phoneNumber">
        /// The phone number.
        /// </param>
        /// <param name="amount">
        /// The amount.
        /// </param>
        /// <returns>
        /// The <see cref="AirtimeResult"/>.
        /// </returns>
        public abstract AirtimeResult TopUp(string phoneNumber, int amount);

        /// <summary>
        /// The save to database.
        /// </summary>
        /// <param name="airtimeResult">
        /// The airtime result.
        /// </param>
        protected void SaveToDatabase(AirtimeResult airtimeResult)
        {
            // save 
            Console.WriteLine("write to database");
        }

        /// <summary>
        /// The create web client with header.
        /// </summary>
        /// <param name="clientIdFieldName">
        /// The client id field name.
        /// </param>
        /// <param name="clientKeyFieldName">
        /// The client key field name.
        /// </param>
        /// <returns>
        /// The <see cref="WebClient"/>.
        /// </returns>
        protected WebClient CreateWebClientWithHeader(string clientIdFieldName = "ClientId", string clientKeyFieldName = "ClientKey")
        {
            WebClient http = new WebClient();

            http.Headers.Add(clientIdFieldName, this.ClientId);
            http.Headers.Add(clientKeyFieldName, this.ClientKey);
            return http;
        }

        /// <summary>
        /// The network response.
        /// </summary>
        /// <param name="http">
        /// The http.
        /// </param>
        /// <param name="url">
        /// The url.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        protected string NetworkResponse(WebClient http, string url)
        {
            // string response = http.DownloadString(url);

            // demo data for test purpose
            int rechargeAmount = Convert.ToInt32(http.QueryString["amount"]);
            var airtimeResult = new AirtimeResult();
            airtimeResult.Id = "123";
            airtimeResult.ResultCode = 200;
            airtimeResult.Balance = 1000;
            airtimeResult.Charge = 1;
            airtimeResult.Message = "asdf";

            var response = JsonConvert.SerializeObject(airtimeResult);
            return response;
        }

        /// <summary>
        /// The network response map to object.
        /// </summary>
        /// <param name="response">
        /// The response.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        protected virtual T NetworkResponseMapToObject<T>(string response)
        {
            T result = JsonConvert.DeserializeObject<T>(response);
            return result;
        }
    }
}
