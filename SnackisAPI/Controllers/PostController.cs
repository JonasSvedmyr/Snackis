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
    public class PostController : Controller
    {
        private UserManager<User> _userManager;
        private Context _context;

        public PostController(UserManager<User> userManager, Context context)
        {
            _userManager = userManager;
            _context = context;
        }
        [Authorize]
        [HttpPost("Post/Create")]
        public async Task<ActionResult> CreateComment([FromBody] CreatePostModel model)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(User.Claims.FirstOrDefault(x => x.Type.Equals(ClaimTypes.Name)).Value);

                var subject = await _context.Subjects.Where(x => x.Id == model.SubjectId).FirstOrDefaultAsync();


                var post = new Post
                {
                    Posted = DateTime.Now,
                    Id = Guid.NewGuid().ToString(),
                    IsReported = false,
                    User = user,
                    Description = model.Description,
                    Title = model.Title,
                    Subject = subject
                };

                await _context.Posts.AddAsync(post);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch
            {
                return BadRequest();
            }

        }
        [Authorize]
        [HttpPost("Post/Edit")]
        public async Task<ActionResult> EditComment([FromBody] EditPostModel model)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(User.Claims.FirstOrDefault(x => x.Type.Equals(ClaimTypes.Name)).Value);

                var post = await _context.Posts.Include(x => x.User).Where(x => x.Id == model.Id).FirstOrDefaultAsync();

                if (user.Id == post.User.Id)
                {
                    post.Description = model.Description;
                    post.Title = model.Title;
                    await _context.SaveChangesAsync();

                    return Ok();
                }
                else
                {
                    return Unauthorized();
                }
            }
            catch
            {

                return BadRequest();
            }
        }

        [Authorize]
        [HttpPost("Post/Report/Create")]
        public async Task<ActionResult> CreateReport([FromBody] CreatePostReportModel model)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(User.Claims.FirstOrDefault(x => x.Type.Equals(ClaimTypes.Name)).Value);

                var post = await _context.Posts.Where(x => x.Id == model.PostId).FirstOrDefaultAsync();

                if (user != null && post != null)
                {
                    var Report = new Report
                    {
                        Id = Guid.NewGuid().ToString(),
                        Post = post,
                        User = user,
                        Reason = model.Reason
                    };

                    _context.Reports.Add(Report);
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
                return StatusCode(500);
            }

        }

        [AllowAnonymous]
        [HttpGet("Post/Get/{id?}")]
        public async Task<ActionResult> GetPosts([FromRoute] string id)
        {
            var post = await _context.Posts.Include(x => x.User).Where(x => x.Id == id).FirstOrDefaultAsync();
            return Ok(new
            {
                id = post.Id,
                title = post.Title,
                description = post.Description,
                user = post.User.UserName,
                posted = post.Posted
            });
        }

        [AllowAnonymous]
        [HttpGet("Post/Get/all/{subjectId?}")]
        public async Task<ActionResult> GetPost([FromRoute] string subjectId)
        {
            var posts = await _context.Posts.Include(x => x.User).Where(x => x.Subject.Id == subjectId).ToListAsync();

            var listOfSubjects = new List<object>();

            foreach (var post in posts)
            {
                listOfSubjects.Add(new
                {
                    id = post.Id,
                    title = post.Title,
                    description = post.Description,
                    user = post.User.UserName,
                    posted = post.Posted
                });
            }
            return Ok(listOfSubjects);
        }

        [Authorize]
        [HttpDelete("Post/Delete/{Id}")]
        public async Task<ActionResult> DeleteComment([FromRoute] string id)
        {
            var user = await _userManager.FindByNameAsync(User.Claims.FirstOrDefault(x => x.Type.Equals(ClaimTypes.Name)).Value);
            //var roles = await _userManager.GetRolesAsync(user);

            Post post = await _context.Posts.Include(x => x.User).Where(x => x.Id == id).FirstOrDefaultAsync();

            if (user.Id == post.User.Id)
            {
                try
                {
                    _context.Posts.Remove(post);
                    await _context.SaveChangesAsync();
                    return Ok();
                }
                catch (Exception)
                {

                    return BadRequest();
                }
            }
            else
            {
                return Unauthorized();
            }
        }
    }
}
