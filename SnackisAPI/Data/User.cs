using Microsoft.AspNetCore.Identity;
using SnackisAPI.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SnackisAPI.Data
{
    public class User : IdentityUser
    {
        public string ImageUri { get; set; }
        public List<Post> Posts { get; set; }
        public List<Comment> Comments { get; set; }
    }
}
