using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WithdrawalService.Domain;
using WithdrawalService.Entities;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using System.Net;

namespace WithdrawalService.Services
{
    public class PaystackHelper
    {
        private readonly RecipientHelper _recipientHelper;

        public PaystackHelper(RecipientHelper recipientHelper)
        {
            _recipientHelper = recipientHelper;
        }
        public async Task<string> CreateRecipient(Paystack _paystack, List<Bank> banks, AppUser user)
        {
            try
            {
                var client = new HttpClient();
                Bank userBank = banks.Where(u => u.BankName == user.BankName).SingleOrDefault();
                string sortCode = $"0{userBank.SortCode}";
                var request = new HttpRequestMessage(HttpMethod.Post, "https://api.paystack.co/transferrecipient");
                request.Headers.Add("Authorization", _paystack.TestSecret);
                
                
                var requestContent = new RequestRecipient
                {
                    type = "nuban",
                    account_number = user.BankAccountNumber,
                    bank_code = sortCode,
                    currency = "NGN",
                    name = user.FullName
                };
                var content = JsonSerializer.Serialize(requestContent);
                request.Content = new StringContent(content);
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();

                if (response.IsSuccessStatusCode)
                {
                    var contentStream = await response.Content.ReadAsStreamAsync();
                    var responseObject = await JsonSerializer.DeserializeAsync<Root>(contentStream);
                    var newRecipient = new Recipient
                    {
                        RecipientCode = responseObject.data.recipient_code,
                        Status = responseObject.status,
                        Message = responseObject.message,
                        Active = responseObject.data.active
                    };
                    var createR = await _recipientHelper.CreateRecipient(newRecipient);
                    return newRecipient.RecipientCode;
                }
                else
                {
                    throw new Exception("A recipient could not be created locally");
                }
            }
            catch (Exception)
            {

                throw;
            }
       }

        public async Task InitiateTransfer(Paystack _paystack, List<Bank> banks, Withdrawal withdrawal, AppUser user, string reciepientVal)
        {
            try
            {
                var client = new HttpClient();
                Bank userBank = banks.Where(u => u.BankName == user.BankName).SingleOrDefault();
                string sortCode = $"0{userBank.SortCode}";
                var request = new HttpRequestMessage(HttpMethod.Post, "https://api.paystack.co/transfer");
                request.Headers.Add("Authorization", _paystack.TestSecret);

                var requestContent = new RequestTransfer
                {
                    source = "balance",
                    amount = withdrawal.Amount.ToString(),
                    bank_code = sortCode,
                    reciepient = reciepientVal
                };
                var content = JsonSerializer.Serialize(requestContent);
                request.Content = new StringContent(content);
                var response = await client.SendAsync(request);

                if (response.StatusCode == HttpStatusCode.BadRequest)
                {
                    await Console.Out.WriteLineAsync("You cannot initiate third party payouts as a starter business");
                }
            } 
            catch (Exception)
            {

                throw;
            }
        }
    }
}
