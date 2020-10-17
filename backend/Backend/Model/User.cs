using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Model
{
    public class User
    {
        public string UID { get; set; }
        public string ConnectionID { get; set; }
        public string Email { get; set; }
        public string ProfilePic { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<User> Friends { get; set; }
        public List<string> Pictures { get; set; }
        public string Bio { get; set; }
    }
}
