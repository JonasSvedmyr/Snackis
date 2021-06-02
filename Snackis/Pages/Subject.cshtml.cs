using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Snackis.Models;
using Snackis.Services;

namespace Snackis.Pages
{
    public class SubjectModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string id { get; set; }
        [BindProperty(SupportsGet = true)]
        public string PostId { get; set; }
        [BindProperty]
        public Models.SubjectModel Subject { get; set; }

        [BindProperty]
        public List<PostModel> Posts { get; set; }
        private readonly ILogger<IndexModel> _logger;
        private readonly ISubjects _subjects;
        private readonly IPosts _posts;

        public SubjectModel(ILogger<IndexModel> logger, ISubjects subjects, IPosts posts)
        {
            _logger = logger;
            _subjects = subjects;
            _posts = posts;
        }

        public async Task<ActionResult> OnGetAsync()
        {
            if (PostId != null)
            {
                await _posts.DeletePost(PostId, HttpContext.Session.GetString("_token"));
            }
            Subject = await _subjects.GetSubject(id);
            Posts = await _posts.GetPosts(id);
            return Page();
        }
    }
}
