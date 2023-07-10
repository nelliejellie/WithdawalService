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
            reference = GenerateReference();
        }

        [Required]
        public string source { get; set; }
        [Required]
        public string amount { get; set; }
        [Required]
        public string reason { get; set; }
        [Required]
        public string bank_code { get; set; }
        [Required]
        public string reciepient { get; set; }
        [Required]
        public string reference { get; set; }

        public static string GenerateReference()
        {
            // Define the length of the reference
            int length = 16;

            // Define the characters to use for the reference
            const string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

            // Generate the reference using random characters
            Random random = new Random();
            string reference = new string(Enumerable.Repeat(characters, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());

            return reference;
        }
    }
}

