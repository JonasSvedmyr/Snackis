using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Snackis.Services;

namespace Snackis.Pages.Auth
{
    public class LoginModel : PageModel
    {
        private IAuthentication _authentication;

        [BindProperty]
        public string Username { get; set; }
        [BindProperty]
        public string Password { get; set; }
        [BindProperty]
        public string ErrorMessage { get; set; }


        public LoginModel(IAuthentication authentication)
        {
            _authentication = authentication;
        }

        public void OnGet()
        {
        }

        public async Task<ActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                (bool success, string result) = await _authentication.Loggin(Username, Password);
                if (success)
                {
                    HttpContext.Session.SetString("_token", result);
                    //return PartialView("");
                    return RedirectToPage("/Index");
                }
                else
                {
                    ErrorMessage = result;
                    return Page();
                }


            }
            return Page();
        }
    }
}
