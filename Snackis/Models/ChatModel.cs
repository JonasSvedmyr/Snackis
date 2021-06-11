using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Snackis.Models
{
    public class ChatModel
    {
        public string Id { get; set; }
        public List<MessageModel> Messages { get; set; }
        
    }
}
