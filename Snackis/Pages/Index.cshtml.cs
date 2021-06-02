using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Snackis.Models;
using Snackis.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Snackis.Pages
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public List<Models.SubjectModel> Subjects { get; set; }
        [BindProperty]
        public Models.SiteModel Site { get; set; }

        private readonly ILogger<IndexModel> _logger;
        private readonly ISubjects _subjects;
        private readonly IAuthentication _authentication;
        private readonly ISite _site;

        public IndexModel(ILogger<IndexModel> logger, ISubjects subjects, IAuthentication authentication, ISite site)
        {
            _logger = logger;
            _subjects = subjects;
            _authentication = authentication;
            _site = site;
        }

        public async Task<ActionResult> OnGetAsync()
        {
            Subjects = await _subjects.GetSubjects();

            Site = await _site.GetTitle();
            return Page();
        }
    }
}
