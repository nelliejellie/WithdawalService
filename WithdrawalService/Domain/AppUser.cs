using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WithdrawalService.Domain
{
    public class AppUser : IdentityUser
    {
        public string? FullName { get; set; }
        public string? CreatedAt { get; set; }
        public string BankAccountNumber { get; set; }
        public string BankName { get; set; }
        [Required(ErrorMessage = "Pin is required.")]
        [StringLength(4, MinimumLength = 4, ErrorMessage = "Pin must have exactly 4 characters.")]
        [RegularExpression(@"^\d{4}$", ErrorMessage = "Pin must contain exactly 4 numeric digits.")]
        public string? Pin { get; set; }
    }
}
