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
    public class ReportModel : PageModel
    {
        private readonly IAuthentication _authentication;
        private readonly IPosts _posts;
        private readonly IComments _comments;

        [BindProperty(SupportsGet = true)]
        public string CommentId { get; set; }
        [BindProperty(SupportsGet = true)]
        public string PostId { get; set; }
        public ReportCommentModel Comment { get; set; }
        public ReportPostModel Post { get; set; }

        public ReportModel(IAuthentication authentication, IPosts posts, IComments comments)
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
                    if(PostId != null)
                    {
                        Post = await _posts.GetReportedPost(PostId, HttpContext.Session.GetString("_token"));
                    }
                    else if(CommentId != null)
                    {
                        Comment = await _comments.GetReportedComment(CommentId, HttpContext.Session.GetString("_token"));
                    }
                    else
                    {
                        RedirectToPage("/index");
                    }
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

        public async Task<ActionResult> OnPostAsync()
        {
            var test = Request.Form["PostReport"];
            var test2 = Request.Form["CommentReport"];
            if (Request.Form["PostReport"].ToString() != "")
            {
                await _posts.RemoveReport(PostId, HttpContext.Session.GetString("_token"));
                return RedirectToPage("/Admin/Reports");
            }
            else if (Request.Form["CommentReport"].ToString() != "")
            {
                await _comments.RemoveReport(CommentId, HttpContext.Session.GetString("_token"));
                return RedirectToPage("/Admin/Reports");
            }
            else if (Request.Form["Post"].ToString() != "")
            {
                await _posts.RemoveReportedPost(Request.Form["Post"], HttpContext.Session.GetString("_token"));
                return RedirectToPage("/Admin/Reports");
            }
            else if (Request.Form["Comment"].ToString() != "")
            {
                await _comments.RemoveReportedComment(Request.Form["Comment"], HttpContext.Session.GetString("_token"));
                return RedirectToPage("/Admin/Reports");
            }
            else
            {
                return Page();
            }
        }
    }
}
