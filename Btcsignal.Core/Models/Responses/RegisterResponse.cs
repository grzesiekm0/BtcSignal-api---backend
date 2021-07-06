using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Btcsignal.Core.Models.Responses
{
    public class RegisterResponse
    {
        [Required]
        [StringLength(50)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 5)]
        public string Password { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 5)]
        public string ConfirmPassword { get; set; }

        [StringLength(50, MinimumLength = 4)]
        public string Roles { get; set; }
    }
}
