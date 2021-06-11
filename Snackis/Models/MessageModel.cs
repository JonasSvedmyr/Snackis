using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Snackis.Models
{
    public class MessageModel
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public DateTime Sent { get; set; }
        public string UserId { get; set; }
        public string Username { get; set; }
    }
}
