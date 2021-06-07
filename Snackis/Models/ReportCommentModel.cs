using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Snackis.Models
{
    public class ReportCommentModel
    {
        public string Id { get; set; }
        public string Reason { get; set; }
        public string UserId { get; set; }
        public string Username { get; set; }
        public Comment ReportedComment { get; set; }
    }
   
    public class Comment
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public string Username { get; set; }
    }
}
