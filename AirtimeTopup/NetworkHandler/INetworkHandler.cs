using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirtimeTopup.Models;

namespace AirtimeTopup.NetworkHandler
{
    public interface INetworkHandler
    {
        BalanceResult GetBalance();
        AirtimeResult TopUp(string phoneNumber, int amount);
    }
}
