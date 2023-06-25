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
    public class UserHelper
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        public UserHelper(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        public async Task<AppUser> GetUsersByAppUserId(string userId)
        {
            try
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var _dbContext = scope.ServiceProvider.GetService<AppDbContext>();

                    var user = await _dbContext.AspNetUsers.Where(u => u.Id == userId).SingleOrDefaultAsync();
                    return user;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
