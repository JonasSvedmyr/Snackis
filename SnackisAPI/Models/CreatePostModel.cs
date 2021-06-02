using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SnackisAPI.Models
{
    public class CreatePostModel
    {
        public string SubjectId { get; set; }  
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
