using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SnackisAPI.Data;
using SnackisAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SnackisAPI.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly Context _context;

        public UserController(UserManager<User> userManager, Context context)
        {
            _userManager = userManager;
            _context = context;
        }
        [Authorize]
        [HttpPost("User/Picture/Set")]
        public async Task<ActionResult> SetProfilePicture([FromBody] SetProfilePictureModel model)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(User.Claims.FirstOrDefault(x => x.Type.Equals(ClaimTypes.Name)).Value);

                if (!string.IsNullOrEmpty(model.ImageUrl))
                {
                    user.ImageUri = model.ImageUrl;
                    await _context.SaveChangesAsync();
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
            catch
            {
                return BadRequest();
            }

        }
        [Authorize]
        [HttpGet("User/Picture/Get")]
        public async Task<ActionResult> GetProfilePicture()
        {
            try
            {
                var user = await _userManager.FindByNameAsync(User.Claims.FirstOrDefault(x => x.Type.Equals(ClaimTypes.Name)).Value);
                var ImageUrl = user.ImageUri;
                return Ok(new 
                {
                    ImageUrl = ImageUrl,
                });
            }
            catch
            {
                return BadRequest();
            }

        }
    }
}
