using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WithdrawalService.Data;
using WithdrawalService.Domain;

namespace WithdrawalService.Services
{
    public class BankHelper
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        public BankHelper( IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }


        public async Task<List<Withdrawal>> GetAllWithdrawalAsync()
        {
            try
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var _dbContext = scope.ServiceProvider.GetService<AppDbContext>();

                    var wits = await _dbContext.Withdrawals.ToListAsync();
                    return wits;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            

        }

        public async Task<List<Bank>> GetAllBanksAsync()
        {
            try
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var _dbContext = scope.ServiceProvider.GetService<AppDbContext>();

                    var banks = await _dbContext.Banks.ToListAsync();
                    return banks;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
                
        }

        public async Task<string> GetBankSortCodeAsync(string Bank)
        {
           
            try
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var _dbContext = scope.ServiceProvider.GetService<AppDbContext>();

                    var bank = await _dbContext.Banks.Where(x => x.BankName == Bank).SingleOrDefaultAsync();
                    return $"0{bank.SortCode}";
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
