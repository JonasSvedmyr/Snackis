using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Snackis.Services;

namespace Snackis.Pages.Admin.Subjects
{
    public class CreateModel : PageModel
    {
        private readonly ISubjects _subjects;
        private readonly IAuthentication _authentication;

        [BindProperty(SupportsGet = true)]
        public string subjectId { get; set; }

        [BindProperty]
        public string Title { get; set; }
        [BindProperty]
        public string Description { get; set; }
        [BindProperty]
        public string ErrorMessage { get; set; }

        public CreateModel(ISubjects subjects, IAuthentication authentication)
        {
            _subjects = subjects;
            _authentication = authentication;
        }
        public async Task<ActionResult> OnGetAsync()
        {
            if (HttpContext.Session.GetString("_token") != null)
            {
                if (await _authentication.IsAdmin(HttpContext.Session.GetString("_token")))
                {
                    return Page();
                }
                else
                {
                    return RedirectToPage("/index");
                }

            }
            else
            {
                return RedirectToPage("/index");
            }
        }

        public async Task<ActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                if (await _authentication.IsAdmin(HttpContext.Session.GetString("_token")))
                {
                    (string result, bool success) = await _subjects.CreateSubject(Title,Description, HttpContext.Session.GetString("_token"));
                    if (success)
                    {
                        return RedirectToPage("/Admin/Management");
                    }
                    else
                    {
                        ErrorMessage = result;
                        return Page();
                    }
                }
                else
                {
                    return RedirectToPage("/index");
                }

            }
            else
            {
                return Page();
            }
        }

    }
}
