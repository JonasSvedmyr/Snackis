using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Snackis.Services
{
    public interface IAuthentication
    {
        Task<(bool, string)> Register(string username,string email, string password);
        Task<(bool, string)> Loggin(string user, string password);

        Task<bool> IsAdmin(string token);
        Task<string> GetUser(string token);
    }
}
