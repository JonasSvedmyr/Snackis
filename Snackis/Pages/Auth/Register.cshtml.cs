using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.DependencyInjection;
using Snackis.Services;

namespace Snackis.Pages.Auth
{
    public class RegisterModel : PageModel
    {
        private IAuthentication _authentication;

        [BindProperty]
        public string Username { get; set; }
        [BindProperty]
        public string Email { get; set; }
        [BindProperty]
        public string Password { get; set; }
        [BindProperty]
        public string ConfirmPassword { get; set; }

        [BindProperty]
        public string ErrorMessage { get; set; }

        public RegisterModel(IAuthentication authentication)
        {
            _authentication = authentication;
        }

        public async Task<ActionResult> OnGetAsync()
        {
            return Page();
        }

        public async Task<ActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                if (Password == ConfirmPassword)
                {
                    (bool success, string result) = await _authentication.Register(Username, Email, Password);
                    if (success)
                    {
                        return RedirectToPage("/auth/login");
                    }
                    else
                    {
                        ErrorMessage = result;
                        return Page();
                    }
                }
                else
                {
                    ErrorMessage = "Password did not match";
                    return Page();
                }
            }
            else
            {
                return Page();
            }
        }
    }
}
