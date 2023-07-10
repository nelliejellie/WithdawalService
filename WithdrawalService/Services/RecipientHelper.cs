using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WithdrawalService.Data;
using WithdrawalService.Entities;
using WithdrawalService.Migrations;

namespace WithdrawalService.Services
{
    public class RecipientHelper
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        public RecipientHelper(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        public async Task<bool> CreateRecipient(Recipient recipient)
        {
            try
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var _dbContext = scope.ServiceProvider.GetService<AppDbContext>();

                    var newRecipientTask = await  _dbContext.Recipients.AddAsync(recipient);
                    var isAdded =  await _dbContext.SaveChangesAsync();

                    return isAdded > 0;
                }
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        public async Task<bool> DeleteRecipient(Recipient recipient)
        {
            try
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var _dbContext = scope.ServiceProvider.GetService<AppDbContext>();

                    var unwantedRecipientTask = _dbContext.Recipients.Remove(recipient);
                    var isAdded = await _dbContext.SaveChangesAsync();

                    return isAdded > 0;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<Recipient> GetRecipientByCode(string code)
        {
            try
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var _dbContext = scope.ServiceProvider.GetService<AppDbContext>();

                    var thatRecipient = await _dbContext.Recipients.Where(r => r.RecipientCode == code).SingleOrDefaultAsync();
                    return thatRecipient;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
