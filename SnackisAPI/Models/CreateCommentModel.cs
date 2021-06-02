using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SnackisAPI.Models
{
    public class CreateCommentModel
    {
        public string PostId { get; set; }
        public string Comment { get; set; }
    }
}
