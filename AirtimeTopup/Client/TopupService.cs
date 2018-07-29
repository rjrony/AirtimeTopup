using System;
using AirtimeTopup.Client.Models;
using AirtimeTopup.Models;

namespace AirtimeTopup.Client
{
    public class TopupService
    {
        private Customer customer;

        public TopupService(Customer customer)
        {
            this.customer = customer;
        }

        public BalanceResult Balance()
        {

            return new BalanceResult()
            {
                Balance = this.customer.Balance,
                Discount = this.customer.Discount
            };
        }

        public AirtimeResult TopUp(string phoneNumber, int amount)
        {
            var airtimeResult = new AirtimeResult()
            {
                ResultCode = 400
            };

            if (customer.Balance < amount)
            {
                Console.WriteLine("Running out of balance");
                airtimeResult.ResultCode = 400;
                airtimeResult.Message = "Running out of balance";
                return airtimeResult;
            }

            var rechargeService = new RechargeService();
            var isRecharged = rechargeService.TopUp(phoneNumber, amount);
            if (isRecharged)
            {
                customer.Balance = customer.Balance - (amount * (100.0 + ConstantValues.Charge) / 100.0)
                                                       + (amount * (customer.Discount) / 100.0);
                airtimeResult = new AirtimeResult()
                {
                    ResultCode = 200,
                    Amount = amount,
                    Balance = customer.Balance,
                    Charge = ConstantValues.Charge,
                    Message = "sceessfully recharge"
                };
                return airtimeResult;
            }
            return airtimeResult;//or throw exception
        }
    }
}