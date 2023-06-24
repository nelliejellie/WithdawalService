using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WithdrawalService.Domain
{
    public class Withdrawal : BaseEntity
    {
        public string? AppUserId { get; set; }
        public string? Status { get; set; }
        public decimal Amount { get; set; }
    }
}
