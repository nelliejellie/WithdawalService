using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WithdrawalService.Services
{
    public class TransferHelper
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        public TransferHelper(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }
    }
}
