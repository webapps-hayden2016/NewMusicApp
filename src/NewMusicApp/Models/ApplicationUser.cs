using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewMusicApp.Models
{
    public class ApplicationUser : IdentityUser
    {
        public DateTime DateJoined { get; set; }
    }
}
