using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SnackisAPI.Models
{
    public class CreateMessageModel
    {
        public string UserId { get; set; }
        public string Message { get; set; }
    }
}
