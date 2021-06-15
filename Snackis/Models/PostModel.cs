using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Snackis.Models
{
    public class PostModel
    {
        public string id { get; set; }
        public string title { get; set; }
        public string ImageUri { get; set; }
        public string description { get; set; }
        public string user { get; set; }
        public DateTime posted { get; set; }
    }
}
