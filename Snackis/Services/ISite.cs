using Snackis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Snackis.Services
{
    public interface ISite
    {
        Task<bool> SetTitle(string Title, string token);
        Task<SiteModel> GetTitle();
    }
}
