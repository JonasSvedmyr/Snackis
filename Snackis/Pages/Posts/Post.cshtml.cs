using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Snackis.Pages.Posts
{
    public class PostModel : PageModel
    {
        private readonly Services.IPosts _post;
        private readonly Services.IComments _comments;

        [BindProperty(SupportsGet = true)]
        public string id { get; set; }
        [BindProperty(SupportsGet = true)]
        public string CommentId { get; set; }

        public Models.PostModel post { get; set; }
        public List<Models.CommentsModel> Comments { get; set; }

        public PostModel(Services.IPosts post, Services.IComments comments)
        {
            _post = post;
            _comments = comments;
        }
        public async Task<ActionResult> OnGetAsync()
        {
            if (CommentId != null)
            {
                await _comments.DeleteComment(CommentId, HttpContext.Session.GetString("_token"));
            }

            post = await _post.GetPost(id);
            Comments = await _comments.GetComments(id);
            return Page();
        }
    }
}
