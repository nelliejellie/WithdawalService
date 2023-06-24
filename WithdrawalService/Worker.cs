using Microsoft.EntityFrameworkCore;
using WithdrawalService.Data;
using WithdrawalService.Domain;
using System.Linq;

namespace WithdrawalService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        //private readonly AppDbContext _context;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public Worker(ILogger<Worker> logger, IServiceScopeFactory serviceScopeFactory)
        {
            _logger = logger;
            //_context = context;
            _serviceScopeFactory = serviceScopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

               
                GetWithdrawal().ForEach(w =>
                {
                    _logger.LogInformation($"this is the id {w.Id} and this is the amount {w.Status}");
                });
                await Task.Delay(2000, stoppingToken);
            }
        }

        private List<Withdrawal> GetWithdrawal()
        {
            try
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var dbContext = scope.ServiceProvider.GetService<AppDbContext>();
                    var withdawals = dbContext.Withdrawals.Where(x => x.Status.Equals("Pending")).ToList();
                    return withdawals;
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                _logger.LogInformation(ex.Message);
                throw;
            }
           
            

        }
    }
}