using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Model
{
    public class PhotoCardData
    {
        public string Image { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Likes { get; set; }

        public PhotoCardData(string image, string firstName, string lastName, int likes)
        {
            this.Image = image;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Likes = likes;
        }
    }
}
