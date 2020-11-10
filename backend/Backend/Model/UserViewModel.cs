using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Model
{
    public class UserViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        public UserViewModel(string FirstName, string LastName, string Email)
        {
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.Email = Email;
        }
    }
}
