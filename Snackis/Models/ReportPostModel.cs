using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Snackis.Models
{
    public class ReportPostModel
    {
        public string Id { get; set; }
        public string Reason { get; set; }
        public string UserId { get; set; }
        public string Username { get; set; }
        public Post ReportedPost { get; set; }
    }
    public class Post
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Username { get; set; }
    }

}
