using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Model
{
    public class Message
    {
        [Key]
        public string UID { get; set; }
        public string Sender { get; set; }
        public string Receiver { get; set; }
        public string Msg { get; set; }
        public DateTime Date { get; set; }
    }
}
    