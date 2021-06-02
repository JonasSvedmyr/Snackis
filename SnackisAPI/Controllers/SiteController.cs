using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SnackisAPI.Data;
using SnackisAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SnackisAPI.Controllers
{
    public class SiteController : Controller
    {
        private Context _context;
        private UserManager<User> _userManager;

        public SiteController(Context context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        [AllowAnonymous]
        [HttpGet("/Title/Get")]
        public async Task<ActionResult> GetSiteTitle()
        {
                try
                {
                    var siteContent = await _context.SiteContent.FirstOrDefaultAsync();
                    return Ok(new {Title = siteContent.Title });
                }
                catch
                {
                    return StatusCode(500);
                }
        }
        [Authorize]
        [HttpPost("/Title/set")]
        public async Task<ActionResult> SetSiteTitle([FromBody] SetTitleModel model)
        {
            var user = await _userManager.FindByNameAsync(User.Claims.FirstOrDefault(x => x.Type.Equals(ClaimTypes.Name)).Value);
            var roles = await _userManager.GetRolesAsync(user);

            if (roles.Contains("root"))
            {
                try
                {
                    var siteContent = await _context.SiteContent.FirstOrDefaultAsync();
                    siteContent.Title = model.Title;
                    await _context.SaveChangesAsync();

                    return Ok();
                }
                catch
                {
                    return StatusCode(500);
                }

            }
            else
            {
                return Unauthorized();
            }
        }
        [Authorize]
        [HttpDelete]
        public async Task<ActionResult> DeleteReportedPost()
        {
            throw new NotImplementedException();
        }
    }
}
