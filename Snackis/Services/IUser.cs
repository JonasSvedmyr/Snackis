using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Snackis.Services
{
    public interface IUser
    {
        Task<string> GetProfilePicture(string token);
        Task<bool> SetProfilePicture(string ImageUrl,string token);
    }
}
