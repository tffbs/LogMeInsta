using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Model
{
    public class ApplicationUser : IdentityUser
    {
        [NotNull]
        public string ProfilePic { get; set; }
    }
}
