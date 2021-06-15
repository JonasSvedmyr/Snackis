using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Snackis.Services;

namespace Snackis.Pages.User
{
    public class EditModel : PageModel
    {
        private IWebHostEnvironment _environment;
        private readonly IUser _user;
        [BindProperty]
        public string ImageUrl { get; set; }

        [BindProperty]
        public IFormFile Upload { get; set; }

        public EditModel(IWebHostEnvironment environment, IUser user)
        {
            _environment = environment;
            _user = user;
        }
        public async Task<ActionResult> OnGetAsync()
        {
            ImageUrl = await _user.GetProfilePicture(HttpContext.Session.GetString("_token"));
            return Page();
        }
        public async Task<ActionResult> OnPostAsync()
        {
            var file = Path.Combine(_environment.WebRootPath, "Images", Upload.FileName);
            using (var fileStream = new FileStream(file, FileMode.Create))
            {
                await Upload.CopyToAsync(fileStream);
                var success = await _user.SetProfilePicture(Upload.FileName, HttpContext.Session.GetString("_token"));
                if (success)
                {
                    return RedirectToPage("/User/Edit");
                }
            }
            return Page();
        }


    }
}
