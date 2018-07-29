// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NetworkMapper.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the NetworkMapper type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace AirtimeTopup.Models
{
    using System;
    using System.Collections.Generic;

    using AirtimeTopup.NetworkHandler;

    /// <summary>
    /// The network mapper.
    /// </summary>
    public class NetworkMapper
    {
        /// <summary>
        /// The mapper.
        /// </summary>
        private readonly Dictionary<Network, INetworkHandler> mapper = new Dictionary<Network, INetworkHandler>();

        /// <summary>
        /// Initializes a new instance of the <see cref="NetworkMapper"/> class.
        /// </summary>
        public NetworkMapper()
        {
            this.mapper.Add(Network.GP, new GpNetworkHandler());
            this.mapper.Add(Network.AIR, new AirtelNetworkHandler());
            this.mapper.Add(Network.BL, new GpNetworkHandler());
            this.mapper.Add(Network.ROBI, new GpNetworkHandler());
        }

        /// <summary>
        /// The get network handler.
        /// </summary>
        /// <param name="phoneNumber">
        /// The phone number.
        /// </param>
        /// <returns>
        /// The <see cref="INetworkHandler"/>.
        /// </returns>
        public INetworkHandler GetNetworkHandler(string phoneNumber)
        {
            var net = this.NetworkSelector(phoneNumber);

            //this.mapper.TryGetValue(net, out INetworkHandler networkHandler);
            //return networkHandler;
            INetworkHandler networkHandler = new GpNetworkHandler();
            return networkHandler;
        }

        /// <summary>
        /// The get network handler.
        /// </summary>
        /// <param name="net">
        /// The net.
        /// </param>
        /// <returns>
        /// The <see cref="INetworkHandler"/>.
        /// </returns>
        //public INetworkHandler GetNetworkHandler(Network net)
        //{
        //    this.mapper.TryGetValue(net, out INetworkHandler networkHandler);
        //    return networkHandler;
        //}

        /// <summary>
        /// The network selector.
        /// </summary>
        /// <param name="phoneNumber">
        /// The phone number.
        /// </param>
        /// <returns>
        /// The <see cref="Network"/>.
        /// </returns>
        /// <exception cref="Exception">
        /// </exception>
        private Network NetworkSelector(string phoneNumber)
        {
            switch (phoneNumber.Substring(0, 3))
            {
                case "016":
                    return Network.AIR;

                case "017":
                    return Network.GP;

                case "019":
                    return Network.BL;

                case "018":
                    return Network.ROBI;

                default:
                    throw new Exception("Unknown Network");
            }
        }
    }
}
