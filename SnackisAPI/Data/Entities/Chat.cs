using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SnackisAPI.Data.Entities
{
    public class Chat
    {
        public string Id { get; set; }
        [ForeignKey("User1Id")]
        public User User { get; set; }
        [ForeignKey("User2Id")]
        public User User2 { get; set; }
        public List<Message> Messages { get; set; }
    }
}
