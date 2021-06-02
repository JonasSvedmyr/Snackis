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
    public class ManagementModel : PageModel
    {
        private readonly IAuthentication _authentication;
        private readonly ISubjects _subjects;
        private readonly ISite _site;
        [BindProperty]
        public SiteModel Site { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SubjectId { get; set; }

        public List<Models.SubjectModel> Subjects { get; set; }

        public ManagementModel(IAuthentication authentication, ISubjects subjects, ISite site)
        {
            _authentication = authentication;
            _subjects = subjects;
            _site = site;
        }
        public async Task<ActionResult> OnGetAsync()
        {
            if (HttpContext.Session.GetString("_token") != null)
            {
                if (await _authentication.IsAdmin(HttpContext.Session.GetString("_token")))
                {
                    if (SubjectId != null)
                    {
                        await _subjects.DeleteSubject(SubjectId, HttpContext.Session.GetString("_token"));
                    }

                    Subjects = await _subjects.GetSubjects();
                    Site = await _site.GetTitle();
                    return Page();
                }
                else
                {
                    return RedirectToPage("/index");
                }

            }
            else
            {
                return RedirectToPage("/index");
            }

        }

        public async Task<ActionResult> OnPostAsync()
        {
            if (HttpContext.Session.GetString("_token") != null)
            {
                if (await _authentication.IsAdmin(HttpContext.Session.GetString("_token")))
                {
                        bool success = await _site.SetTitle(Site.Title, HttpContext.Session.GetString("_token"));
                        return RedirectToPage("/Admin/Management");
                    }
                else
                {
                    return RedirectToPage("/index");
                }

            }
            else
            {
                return RedirectToPage("/index");
            }
        }
    }
}
