using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SnackisAPI.Data;
using SnackisAPI.Data.Entities;
using SnackisAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SnackisAPI.Controllers
{
    public class SubjectController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly Context _context;

        public SubjectController(UserManager<User> userManager, Context context)
        {
            _userManager = userManager;
            _context = context;
        }

        [Authorize]
        [HttpPost("Subject/Add")]
        public async Task<ActionResult> AddSubject([FromBody] SubjectModel model)
        {
            var user = await _userManager.FindByNameAsync(User.Claims.FirstOrDefault(x => x.Type.Equals(ClaimTypes.Name)).Value);
            var roles = await _userManager.GetRolesAsync(user);

            if (roles.Contains("root"))
            {
                try
                {
                    var Subject = new Subject
                    {
                        Id = Guid.NewGuid().ToString(),
                        Title = model.Title,
                        Description = model.Description
                    };

                    _context.Subjects.Add(Subject);
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
        [HttpPost("Subject/Edit")]
        public async Task<ActionResult> EditSubject([FromBody] EditSubjectModel model)
        {
            var user = await _userManager.FindByNameAsync(User.Claims.FirstOrDefault(x => x.Type.Equals(ClaimTypes.Name)).Value);
            var roles = await _userManager.GetRolesAsync(user);

            if (roles.Contains("root"))
            {
                try
                {
                    var subject = await _context.Subjects.Where(x => x.Id == model.Id).FirstOrDefaultAsync();

                    subject.Title = model.Title;
                    subject.Description = model.Description;

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

        [AllowAnonymous]
        [HttpGet("Subject/Get")]
        public async Task<ActionResult> GetSubjects()
        {
            var subjects = await _context.Subjects.ToListAsync();

            var listOfSubjects = new List<object>();

            foreach (var subject in subjects)
            {
                listOfSubjects.Add(new
                {
                    id = subject.Id,
                    title = subject.Title,
                    description = subject.Description
                });
            }

            return Ok(listOfSubjects);

        }

        [AllowAnonymous]
        [HttpGet("Subject/Get/{id?}")]
        public async Task<ActionResult> GetSubject([FromRoute] string id)
        {
            var subject = await _context.Subjects.Where(x => x.Id == id).FirstOrDefaultAsync();

            return Ok(new {
                id = subject.Id,
                title = subject.Title,
                description = subject.Description
            });
        }

        [Authorize]
        [HttpDelete("Subject/Delete/{id}")]
        public async Task<ActionResult> DeleteSubject([FromRoute] string id)
        {
            var user = await _userManager.FindByNameAsync(User.Claims.FirstOrDefault(x => x.Type.Equals(ClaimTypes.Name)).Value);
            var roles = await _userManager.GetRolesAsync(user);

            if (roles.Contains("root"))
            {
                var subject = await _context.Subjects.Where(x => x.Id == id).FirstOrDefaultAsync();
                _context.Subjects.Remove(subject);
                await _context.SaveChangesAsync();
                return Ok();
            }
            else
            {
                return Unauthorized();
            }
        }
    }
}
