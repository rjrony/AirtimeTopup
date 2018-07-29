// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Program.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the Program type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using AirtimeTopup.Client;
using AirtimeTopup.Notification;

namespace AirtimeTopup
{
    using System;

    /// <summary>
    /// The program.
    /// </summary>
    class Program
    {
        /// <summary>
        /// The main.
        /// </summary>
        /// <param name="args">
        /// The args.
        /// </param>
        static void Main(string[] args)
        {
            Subscriber sub = new Subscriber(EventAggregator.Instance);

            RechargeService rechargeService = new RechargeService();
            var phoneNumber = "01757294407";
            var rechargeAmount = 20;

            try
            {
                //ToDO:payment handle
                rechargeService.TopUp(phoneNumber, rechargeAmount);
                
                Console.WriteLine("Successfully recharge to phoneNumber: {0}, amount: {1}.", phoneNumber, rechargeAmount);
                //Console.ReadKey();

                var topupClient1 = new TopupClient("rony", "pass");
                var balance1 = topupClient1.TopupService.Balance();
                Console.WriteLine("Balance:" + balance1.Balance);
                var clientTopUp = topupClient1.TopupService.TopUp(phoneNumber, rechargeAmount);
                Console.WriteLine("Topup returned balance:" + clientTopUp.Balance);
                balance1 = topupClient1.TopupService.Balance();
                Console.WriteLine("Balance:" + balance1.Balance);

                var topupClient2 = new TopupClient("ahad", "pass2");
                var balance2 = topupClient2.TopupService.Balance();
                Console.WriteLine("Balance:" + balance2.Balance);

                var topupClient3 = new TopupClient("rony", "pass");
                var balance3 = topupClient3.TopupService.Balance();
                Console.WriteLine("Balance:" + balance3.Balance);

                var topupClient = new TopupClient("ahad", "pass2");
                var balance = topupClient.TopupService.Balance();
                Console.WriteLine("Balance:" + balance.Balance);
                clientTopUp = topupClient.TopupService.TopUp(phoneNumber, rechargeAmount);
                balance = topupClient.TopupService.Balance();
                Console.WriteLine("Balance:" + balance.Balance);

                var topupClient4 = new TopupClient("rony", "pass");
                var balance4 = topupClient4.TopupService.Balance();
                Console.WriteLine("Balance:" + balance4.Balance);

                topupClient = new TopupClient("ahad", "pass2");
                balance = topupClient.TopupService.Balance();
                Console.WriteLine("Balance:" + balance.Balance);

                Console.ReadKey();
            }
            catch (System.Net.WebException wex)
            {
                Console.WriteLine("HTTP Status: {0} {1}", wex.Status, wex.Message);
            }
        }
    }
}
