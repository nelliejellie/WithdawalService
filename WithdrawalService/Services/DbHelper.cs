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
    public class DbHelper
    {
        private AppDbContext _dbContext;

        private DbContextOptions<AppDbContext> GetAllOptions()
        {
            DbContextOptionsBuilder<AppDbContext> optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

            optionsBuilder.UseNpgsql(AppSettings.ConnectionString);

            return optionsBuilder.Options;
        }

        public  List<Withdrawal> GetWitdrawals()
        {
            using (_dbContext = new AppDbContext(GetAllOptions()))
            {
                try
                {
                    var witdrawals = _dbContext.Withdrawals.ToList();

                    if (witdrawals == null)
                        throw new InvalidOperationException("No user data is found!");

                    return witdrawals;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public async Task<List<Bank>> GetAllBanksAsync()
        {
            using (_dbContext = new AppDbContext(GetAllOptions()))
            {
                var banks = await _dbContext.Banks.ToListAsync();
                return banks;
            }

        }

        public async Task<Bank> GetBankSortCodeAsync(string Bank)
        {
            using (_dbContext = new AppDbContext(GetAllOptions()))
            {
                var bank = await _dbContext.Banks.Where(x => x.BankName == Bank).SingleOrDefaultAsync();
                return bank;
            }
        }
    }
}
