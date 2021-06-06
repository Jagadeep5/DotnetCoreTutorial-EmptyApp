using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;

namespace core_tool_empty.Data
{
    [Keyless]
    public class Login
    {
        [Display(Name = "User ID")]
        [Required(ErrorMessage = "User id is Required")]
        public string UserId { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password is required")]
        
        public string Password { get; set; }

        public string ReturnUrl { get; set; }

        [NotMapped]
        public IList<AuthenticationScheme> ExternalAuths { get; set; }
    }
}
