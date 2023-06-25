using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WithdrawalService.Domain
{
    public class Bank
    {
        public string Id { get; set; }
        public string BankName { get; set; }
        public string SortCode { get; set; }
    }
}
