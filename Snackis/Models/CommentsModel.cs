using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Snackis.Models
{
    public class CommentsModel
    {
        public string id { get; set; }
        public string comment { get; set; }
        public string userid { get; set; }
        public string username { get; set; }
        public DateTime posted { get; set; }
    }
}
