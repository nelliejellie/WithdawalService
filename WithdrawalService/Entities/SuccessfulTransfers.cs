using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WithdrawalService.Entities
{
    public class SuccessfulTransfers
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Source { get; set; }
        [Required]
        public decimal Amount { get; set; }
        [Required]
        public string Recipient { get; set; }
        [Required]
        public string Reason { get; set; }
        [Required]
        public string AccountNumber { get; set; }
        [Required]
        public string SortCode { get; set; }
    }
}

