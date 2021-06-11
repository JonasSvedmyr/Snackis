using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SnackisAPI.Data.Entities
{
    public class Message
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public DateTime Sent { get; set; }
        public string UserId { get; set; }
    }
}
