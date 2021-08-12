using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Btcsignal.Core.Models.Responses
{
    public class UserManagerResponse
    {
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
        public IEnumerable<string> Errors { get; set; }
        public DateTime? ExpireDate { get; set; }

        [StringLength(50, MinimumLength = 4)]
        public string Role { get; set; }
        public string UserId { get; set; }

    }
}
