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

        public string PictureData { get; set; }

        public int Likes { get; set; }

        public string UserId { get; set; }
        
        [NotMapped]
        public virtual User User { get; set; }
    }
}
