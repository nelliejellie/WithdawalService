using Microsoft.EntityFrameworkCore;
using WithdrawalService.Data;
using WithdrawalService.Domain;
using System.Linq;
using WithdrawalService.Services;
using Microsoft.Extensions.Options;

namespace WithdrawalService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly BankHelper _bankHelper;
        private readonly PaystackHelper _paystackHelper;
        private readonly UserHelper _userHelper;
        private readonly Paystack _paystack;

        public Worker(ILogger<Worker> logger,UserHelper userHelper, IOptions<Paystack> options,
            BankHelper bankHelper, PaystackHelper paystackHelper)
        {
            _logger = logger;
            _bankHelper = bankHelper;
            _paystackHelper = paystackHelper;
            _userHelper = userHelper;
            _paystack = options.Value;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

                    var withdrawalsTask = _bankHelper.GetAllWithdrawalAsync();
                    var withdrawals = await withdrawalsTask;

                    var BanksTask = _bankHelper.GetAllBanksAsync();
                    var banks = await BanksTask;


                    withdrawals.ForEach(async w =>
                    {
                        _logger.LogInformation($"this is the id {w.Id} and this is the amount {w.Amount} with status {w.Status}");
                        var userTask = _userHelper.GetUsersByAppUserId(w.AppUserId);
                        var user = await userTask;
                        var recipientCreator = await _paystackHelper.CreateRecipient(_paystack,banks,user);
                        _logger.LogInformation($"a successful recipient has been created for {user.FullName} with account number {user.BankAccountNumber}");
                        if (recipientCreator.Length > 5)
                        {
                            await _paystackHelper.InitiateTransfer(_paystack, banks, w, user, recipientCreator);
                        }
                        

                    });
                    await Task.Delay(3600000, stoppingToken);
                }
                catch (Exception ex)
                {
                    _logger.LogInformation("Error gotten at: {time}", DateTimeOffset.Now);
                    _logger.LogInformation(ex.Message);
                    throw;
                }
                
            }
        }

        
    }
}