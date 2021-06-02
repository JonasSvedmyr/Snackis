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
    public class EditModel : PageModel
    {
        private readonly IComments _comments;

        [BindProperty(SupportsGet = true)]
        public string Id { get; set; }

        [BindProperty]
        public string Comment { get; set; }
        [BindProperty]
        public string ErrorMessage { get; set; }

        public EditModel(IComments comments)
        {
            _comments = comments;
        }
        public async Task<ActionResult> OnGetAsync()
        {
            if (HttpContext.Session.GetString("_token") != null)
            {
                if (Id != null)
                {
                    var post = await _comments.GetComment(Id);

                    Comment = post.comment;
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
            (string message, bool success) = await _comments.EditComment(Comment,Id, HttpContext.Session.GetString("_token"));

            if (success)
            {
                return RedirectToPage("/index", new { id = Id });
            }
            else
            {
                ErrorMessage = message;
                return Page();
            }
        }
    }
}
