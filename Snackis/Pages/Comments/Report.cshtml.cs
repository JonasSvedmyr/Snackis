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
    public class ReportModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string id { get; set; }
        [BindProperty]
        public string Report { get; set; }
        [BindProperty]
        public string ErrorMessage { get; set; }

        public IComments _comments { get; set; }

        public ReportModel(IComments comments)
        {
            _comments = comments;
        }
        public async Task<ActionResult> OnGet()
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
            (_, bool success) = await _comments.CreateReport(Report, id, HttpContext.Session.GetString("_token"));
            if (success)
            {
                return RedirectToPage("/Index");
            }
            else
            {
                return Page();
            }
        }
    }
}
