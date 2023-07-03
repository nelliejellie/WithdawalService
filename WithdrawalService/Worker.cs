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
        private readonly UserHelper _userHelper;
        private readonly Paystack _paystack;

        public Worker(ILogger<Worker> logger,UserHelper userHelper, IOptions<Paystack> options, BankHelper bankHelper)
        {
            _logger = logger;
            _bankHelper = bankHelper;
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

                    banks.ForEach(b =>
                    {
                        _logger.LogInformation($"this is {b.BankName}");
                    });

                    withdrawals.ForEach(async w =>
                    {
                        _logger.LogInformation($"this is the id {w.Id} and this is the amount {w.Status}");
                        var userTask = _userHelper.GetUsersByAppUserId(w.AppUserId);
                        var user = await userTask;
                        _logger.LogInformation($"this withdrawal belongs to {user.FullName} with account number {user.BankAccountNumber}");

                    });
                    await Task.Delay(5000, stoppingToken);
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