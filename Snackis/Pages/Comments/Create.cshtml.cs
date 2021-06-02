using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Snackis.Services;

namespace Snackis.Pages.Comments
{
    public class CreateModel : PageModel
    {
        private readonly IComments _comments;

        [BindProperty(SupportsGet = true)]
        public string PostId { get; set; }

        [BindProperty]
        public string Comment { get; set; }
        [BindProperty]
        public string ErrorMessage { get; set; }

        public CreateModel(IComments comments)
        {
            _comments = comments;
        }
        public async Task<ActionResult> OnGetAsync()
        {
            if (HttpContext.Session.GetString("_token") != null)
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
            (string message, bool success) = await _comments.CreateComment(Comment, PostId, HttpContext.Session.GetString("_token"));

            if (success)
            {
                return RedirectToPage("/Posts/Post", new { id = PostId });
            }
            else
            {
                ErrorMessage = message;
                return Page();
            }
        }
    }
}
