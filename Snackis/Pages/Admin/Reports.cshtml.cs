using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Snackis.Models;
using Snackis.Services;

namespace Snackis.Pages.Admin
{
    public class ReportsModel : PageModel
    {
        private readonly IAuthentication _authentication;
        private readonly IPosts _posts;
        private readonly IComments _comments;

        [BindProperty(SupportsGet = true)]
        public string CommentId { get; set; }
        [BindProperty(SupportsGet = true)]
        public string PostId { get; set; }
        public List<ReportCommentModel> Comments { get; set; }
        public List<ReportPostModel> posts { get; set; }

        public ReportsModel(IAuthentication authentication, IPosts posts, IComments comments)
        {
            _authentication = authentication;
            _posts = posts;
            _comments = comments;
        }

        public async Task<ActionResult> OnGetAsync()
        {
            if (HttpContext.Session.GetString("_token") != null)
            {
                if (await _authentication.IsAdmin(HttpContext.Session.GetString("_token")))
                {
                    posts = await _posts.GetReportedPosts(HttpContext.Session.GetString("_token"));
                    Comments = await _comments.GetReportedComments(HttpContext.Session.GetString("_token"));
                    return Page();
                }
                else
                {
                    return RedirectToPage("/index");
                }
            }
            else
            {
                return RedirectToPage("/auth/login");
            }
        }
    }
}
