using Microsoft.EntityFrameworkCore;
using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Model
{
    public class User
    {
        [Key]
        public string UID { get; set; }

        [NotNull]
        [StringLength(250)]
        public string ConnectionID { get; set; }

        [NotNull]
        [StringLength(100)]
        public string Email { get; set; }


        [NotNull]
        [StringLength(100)]
        public string FirstName { get; set; }
            
        [NotNull]
        [StringLength(100)]
        public string LastName { get; set; }

        public virtual ICollection<User> Friends { get; set; }

        [NotMapped]
        public virtual ICollection<Picture> Pictures { get; set; }

        public string Bio { get; set; }
    }
}
