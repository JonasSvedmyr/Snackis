using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SnackisAPI.Data.Entities
{
    public class Post
    {
        [Key]
        public string Id { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
        [ForeignKey("SubjectId")]
        public Subject Subject { get; set; }
        public DateTime Posted { get; set; }
        public string Title { get; set; }
        public bool IsReported { get; set; }
        public string Description { get; set; }
    }
}
