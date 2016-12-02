using NewMusicApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NewMusicApp.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Please enter an email address")]
        public string Email { get; set; }

        [Required(ErrorMessage ="Please enter a valid password")]
        public string Password { get; set; }

        [Compare(nameof(Password), ErrorMessage = "Passwords don't match.")]
        public string ConfirmPassword { get; set; }
    }
}
