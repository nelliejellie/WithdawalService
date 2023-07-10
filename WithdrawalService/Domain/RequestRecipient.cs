using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WithdrawalService.Domain
{
    public class RequestRecipient
    {
        [Required]
        public string type { get; set; }
        [Required]
        public string account_number { get; set; }
        [Required]
        public string bank_code { get; set; }
        [Required]
        public string currency { get; set; }
        [Required]
        public string name { get; set; }
    }
}
