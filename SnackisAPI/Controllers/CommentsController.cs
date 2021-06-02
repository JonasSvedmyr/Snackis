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
    public class CommentsController : Controller
    {
        private UserManager<User> _userManager;
        private Context _context;

        public CommentsController(UserManager<User> userManager, Context context)
        {
            _userManager = userManager;
            _context = context;
        }
        [Authorize]
        [HttpPost("Comments/Create")]
        public async Task<ActionResult> CreateComment([FromBody] CreateCommentModel model)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(User.Claims.FirstOrDefault(x => x.Type.Equals(ClaimTypes.Name)).Value);

                var post = await _context.Posts.Where(x => x.Id == model.PostId).FirstOrDefaultAsync();


                var comment = new Comment
                {
                    Posted = DateTime.Now,
                    Id = Guid.NewGuid().ToString(),
                    IsReported = false,
                    User = user,
                    Text = model.Comment,
                    Post = post
                };

                await _context.Comments.AddAsync(comment);
                await _context.SaveChangesAsync();

                return Ok();
            }
            catch
            {

                return BadRequest();
            }

        }
        [Authorize]
        [HttpPost("Comments/Edit")]
        public async Task<ActionResult> EditComment([FromBody] EditCommentModel model)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(User.Claims.FirstOrDefault(x => x.Type.Equals(ClaimTypes.Name)).Value);

                var comment = await _context.Comments.Include(x => x.User).Where(x => x.Id == model.CommentId).FirstOrDefaultAsync();

                if (user.Id == comment.User.Id)
                {
                    comment.Text = model.Comment;
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

        [AllowAnonymous]
        [HttpGet("Comments/Get/{id?}")]
        public async Task<ActionResult> GetPosts([FromRoute] string id)
        {
            var post = await _context.Comments.Include(x => x.User).Where(x => x.Id == id).FirstOrDefaultAsync();
            return Ok(new
            {
                id = post.Id,
                comment = post.Text,
                userid = post.User.Id,
                username = post.User.UserName,
                posted = post.Posted
            });
        }

        [AllowAnonymous]
        [HttpGet("Comments/Get/all/{postId?}")]
        public async Task<ActionResult> GetPost([FromRoute] string postId)
        {
            var posts = await _context.Comments.Include(x => x.User).Where(x => x.Post.Id == postId).ToListAsync();

            var listOfSubjects = new List<object>();

            foreach (var post in posts)
            {
                listOfSubjects.Add(new
                {
                    id = post.Id,
                    comment = post.Text,
                    userid = post.User.Id,
                    username = post.User.UserName,
                    posted = post.Posted
                });
            }
            return Ok(listOfSubjects);
        }

        [Authorize]
        [HttpDelete("Comments/Delete/{id}")]
        public async Task<ActionResult> DeleteComment([FromRoute] string id)
        {
            var user = await _userManager.FindByNameAsync(User.Claims.FirstOrDefault(x => x.Type.Equals(ClaimTypes.Name)).Value);

            Comment comment = await _context.Comments.Include(x => x.User).Where(x => x.Id == id).FirstOrDefaultAsync();

            if (user.Id == comment.User.Id)
            {
                try
                {
                    _context.Comments.Remove(comment);
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
