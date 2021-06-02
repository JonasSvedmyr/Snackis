using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SnackisAPI.Data.Entities
{
    public class Report
    {
        [Key]
        public string Id { get; set; }
        [ForeignKey("CommentId")]
        public Comment Comment { get; set; }
        [ForeignKey("PostId")]
        public Post Post { get; set; }
        [ForeignKey("UserId")]
        public  User User { get; set; }
        public string Reason { get; set; }
    }
}
