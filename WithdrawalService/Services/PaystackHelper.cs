using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WithdrawalService.Domain;

namespace WithdrawalService.Services
{
    public class PaystackHelper
    {
       public static Task<bool> CreateRecipient(Paystack _paystack)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, "https://api.paystack.co/transferrecipient");
            request.Headers.Add("Authorization", _paystack.TestSecret);
        }
    }
}
