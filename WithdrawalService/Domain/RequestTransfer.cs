using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WithdrawalService.Domain
{
    public class RequestTransfer
    {
        public RequestTransfer()
        {
            reason = "Urgent use";
        }

        [Required]
        public string source { get; set; }
        [Required]
        public string amount { get; set; }
        [Required]
        public string reason { get; set; }
        [Required]
        public string account_number { get; set; }
        [Required]
        public string bank_code { get; set; }
    }
}

