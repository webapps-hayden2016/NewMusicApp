using NewMusicApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NewMusicApp.ViewModels
{
    public class LoginViewModel
    {
        public ApplicationUser User { get; set; } 

        public string UserName { get; set; }

        [Required(ErrorMessage = "Enter your email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Enter your password")]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
