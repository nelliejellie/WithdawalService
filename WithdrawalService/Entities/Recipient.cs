using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WithdrawalService.Entities
{
    public class Recipient
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public bool Status { get; set; }
        [Required]
        public  string RecipientCode { get; set; }
        [Required]
        public bool Active { get; set; }
        [Required]
        public string Message { get; set; }
    }
}
