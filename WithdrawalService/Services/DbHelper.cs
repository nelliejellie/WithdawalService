using Microsoft.EntityFrameworkCore;
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

        public List<Withdrawal> GetWitdrawals()
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
    }
}
