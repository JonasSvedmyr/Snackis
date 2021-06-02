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
    public class EditModel : PageModel
    {
        private readonly ISubjects _subjects;
        private readonly IAuthentication _authentication;

        [BindProperty(SupportsGet = true)]
        public string id { get; set; }

        [BindProperty]
        public string Title { get; set; }
        [BindProperty]
        public string Description { get; set; }
        [BindProperty]
        public string ErrorMessage { get; set; }

        public EditModel(ISubjects subjects, IAuthentication authentication)
        {
            _subjects = subjects;
            _authentication = authentication;
        }
        public async Task<ActionResult> OnGetAsync()
        {
            if (await _authentication.IsAdmin(HttpContext.Session.GetString("_token")))
            {
                var subject = await _subjects.GetSubject(id);
                if (subject != null)
                {
                    Title = subject.title;
                    Description = subject.description;
                    return Page();
                }
                else
                {
                    return RedirectToPage("/Admin/Management");
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
                    (string result, bool success) = await _subjects.EditSubject(Title, Description,id, HttpContext.Session.GetString("_token"));
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
