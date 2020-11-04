using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Model
{
    public class FriendRequest
    {
        [Key]
        public string UID { get; set; }
        public virtual ApplicationUser Creator { get; set; }
        public DateTime Time { get; set; }
    }
}
