using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Model
{
    public class ApplicationUser : IdentityUser
    {
        public string ProfilePic { get; set; }

        public string FirstName { get; set; }

        [StringLength(100)]
        public string LastName { get; set; }

        public string Bio { get; set; }

        public virtual ICollection<ApplicationUser> Friends { get; set; }
        public virtual ICollection<Picture> Pictures { get; set; }
        public virtual ICollection<FriendRequest> Requests { get; set; }
    }
}
