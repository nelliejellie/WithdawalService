using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WithdrawalService.Data;
using WithdrawalService.Domain;
using WithdrawalService.Entities;

namespace WithdrawalService.Services
{
    public class TransferHelper
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly ILogger<TransferHelper> _logger;
        public TransferHelper(IServiceScopeFactory serviceScopeFactory, ILogger<TransferHelper> logger)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _logger = logger;
        }

        public async Task<bool> CreateTransferRecord(SuccessfulTransfers tranfer)
        {
            try
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var _dbContext = scope.ServiceProvider.GetService<AppDbContext>();

                    var newRecipientTask = await _dbContext.SuccessfulTransfers.AddAsync(tranfer);
                    var isAdded = await _dbContext.SaveChangesAsync();

                    return isAdded > 0;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task<SuccessfulTransfers> GetRecipientByCode(string code)
        {
            try
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var _dbContext = scope.ServiceProvider.GetService<AppDbContext>();

                    var thatTransfer = await _dbContext.SuccessfulTransfers.Where(r => r.Recipient == code).SingleOrDefaultAsync();
                    return thatTransfer;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }
    }
}
