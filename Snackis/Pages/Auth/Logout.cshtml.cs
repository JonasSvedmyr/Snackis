using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Snackis.Pages.Auth
{
    public class LogoutModel : PageModel
    {
        public async Task<ActionResult> OnGetAsync()
        {
            try
            {
                HttpContext.Session.Remove("_token");
                return RedirectToPage("/Auth/Login");
            }
            catch (Exception)
            {

                return RedirectToPage("/index");
            }
        }
    }
}
