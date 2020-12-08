using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Model
{
    public class Like
    {
        [Key]
        public string UID { get; set; }
        public virtual ApplicationUser Creator { get; set; }
    }
}
