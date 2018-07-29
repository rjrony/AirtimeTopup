// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RechargeService.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the RechargeService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace AirtimeTopup
{
    using AirtimeTopup.Models;
    using AirtimeTopup.NetworkHandler;

    /// <summary>
    /// The recharge service.
    /// </summary>
    public class RechargeService
    {
        /// <summary>
        /// Gets or sets the network handler.
        /// </summary>
        private INetworkHandler NetworkHandler { get; set; }

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
        /// The <see cref="bool"/>.
        /// </returns>
        public bool TopUp(string phoneNumber, int amount)
        {
            var networkMapper = new NetworkMapper();
            this.NetworkHandler = networkMapper.GetNetworkHandler(phoneNumber);

            var response = this.NetworkHandler.TopUp(phoneNumber, amount);

            return response.ResultCode == 200;
        }
    }
}
