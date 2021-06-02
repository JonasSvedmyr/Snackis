using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SnackisAPI.Data.Entities
{
    public class Comment
    {
        [Key]
        public string Id { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
        [ForeignKey("SubjectId")]
        public Post Post { get; set; }
        public DateTime Posted { get; set; }
        public bool IsReported { get; set; }
        public string Text { get; set; }
    }
}
