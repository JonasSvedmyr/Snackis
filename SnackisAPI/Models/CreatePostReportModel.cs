using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SnackisAPI.Models
{
    public class CreatePostReportModel
    {
        public string PostId { get; set; }
        public string Reason { get; set; }
    }
}
