using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;


namespace Backend.Model
{
    public class Picture
    {
        [Key]
        public string UID { get; set; }

        public byte[] PictureData { get; set; }

        public int Likes { get; set; }

        public string UserId { get; set; }
        
        public virtual ApplicationUser User { get; set; }
        public virtual ICollection<Like> Persons { get; set; }
    }
}
