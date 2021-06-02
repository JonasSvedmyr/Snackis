using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Snackis.Services;

namespace Snackis.Pages.Posts
{
    public class CreateModel : PageModel
    {
        private readonly IPosts _posts;

        [BindProperty(SupportsGet = true)]
        public string subjectId { get; set; }

        [BindProperty]
        public string Title { get; set; }
        [BindProperty]
        public string Description { get; set; }
        [BindProperty]
        public string ErrorMessage { get; set; }

        public CreateModel(IPosts posts)
        {
            _posts = posts;
        }
        public async Task<ActionResult> OnGetAsync()
        {
            if(HttpContext.Session.GetString("_token") != null)
            {
                return Page();
            }
            else
            {
                return RedirectToPage("/auth/login");
            }
        }
        public async Task<ActionResult> OnPostAsync()
        {
            (string message, bool success) = await _posts.CreatePost(Title, Description, HttpContext.Session.GetString("_token"), subjectId);

            if (success)
            {
                return RedirectToPage("/Subject", new {id =subjectId});
            }
            else
            {
                ErrorMessage = message;
                return Page();
            }
        }
    }
}
