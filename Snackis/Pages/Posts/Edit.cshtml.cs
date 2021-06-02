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
    public class EditModel : PageModel
    {
        private readonly IPosts _posts;

        [BindProperty(SupportsGet = true)]
        public string Id { get; set; }

        [BindProperty]
        public string Title { get; set; }
        [BindProperty]
        public string Description { get; set; }
        [BindProperty]
        public string ErrorMessage { get; set; }

        public EditModel(IPosts posts)
        {
            _posts = posts;
        }
        public async Task<ActionResult> OnGetAsync()
        {
            if (HttpContext.Session.GetString("_token") != null)
            {
                if(Id != null)
                {
                    var post = await _posts.GetPost(Id);
                    Title = post.title;
                    Description = post.description;
                }
                return Page();
            }
            else
            {
                return RedirectToPage("/auth/login");
            }
        }
        public async Task<ActionResult> OnPostAsync()
        {
            (string message, bool success) = await _posts.EditPost(Title, Description,Id, HttpContext.Session.GetString("_token"));

            if (success)
            {
                return RedirectToPage("/Posts/Post", new { id = Id });
            }
            else
            {
                ErrorMessage = message;
                return Page();
            }
        }
    }
}
