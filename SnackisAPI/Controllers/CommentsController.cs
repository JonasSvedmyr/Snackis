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
                    imageUri = post.User.ImageUri,
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

        [Authorize]
        [HttpPost("Comments/Report/Create")]
        public async Task<ActionResult> CreateReport([FromBody] CreateCommentReportModel model)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(User.Claims.FirstOrDefault(x => x.Type.Equals(ClaimTypes.Name)).Value);

                var comment = await _context.Comments.Where(x => x.Id == model.CommentId).FirstOrDefaultAsync();

                if (user != null && comment != null)
                {
                    var Report = new Report
                    {
                        Id = Guid.NewGuid().ToString(),
                        Comment = comment,
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

        [Authorize]
        [HttpGet("Comments/Report/Get/{id?}")]
        public async Task<ActionResult> GetReportedComment([FromRoute] string id)
        {
            var user = await _userManager.FindByNameAsync(User.Claims.FirstOrDefault(x => x.Type.Equals(ClaimTypes.Name)).Value);
            if (await _userManager.IsInRoleAsync(user, "root"))
            {

                var ReportedComment = await _context.Reports.Include(x => x.User).Include(x => x.Comment).ThenInclude(x => x.User).Where(x => x.Id == id).FirstOrDefaultAsync();

                var comment = new
                {
                    Id = ReportedComment.Comment.Id,
                    text = ReportedComment.Comment.Text,
                    Username = ReportedComment.Comment.User.UserName
                };

                return Ok(new
                {
                    id = ReportedComment.Id,
                    reason = ReportedComment.Reason,
                    userid = ReportedComment.User.Id,
                    username = ReportedComment.User.UserName,
                    reportedComment = comment
                });
            }
            else
            {
                return Unauthorized();
            }
        }

        [Authorize]
        [HttpGet("Comments/Report/Get/All")]
        public async Task<ActionResult> GetReportedComments()
        {
            var user = await _userManager.FindByNameAsync(User.Claims.FirstOrDefault(x => x.Type.Equals(ClaimTypes.Name)).Value);
            if (await _userManager.IsInRoleAsync(user, "root"))
            {

                var Reports = await _context.Reports.Include(x => x.User).Include(x => x.Comment).ThenInclude(x => x.User).Where(x => x.Comment.Id != null).ToListAsync();

                var ListOfComments = new List<object>();

                foreach (var report in Reports)
                {
                    var _comment = new
                    {
                        Id = report.Comment.Id,
                        text = report.Comment.Text,
                        Username = report.Comment.User.UserName
                    };

                    ListOfComments.Add(new
                    {
                        id = report.Id,
                        reason = report.Reason,
                        userid = report.User.Id,
                        username = report.User.UserName,
                        reportedComment = _comment
                    });

                }
                return Ok(ListOfComments);
            }
            else
            {
                return Unauthorized();
            }
        }

        [Authorize]
        [HttpDelete("Comments/Report/Remove/{id?}")]
        public async Task<ActionResult> RemoveReport([FromRoute] string id)
        {
            var user = await _userManager.FindByNameAsync(User.Claims.FirstOrDefault(x => x.Type.Equals(ClaimTypes.Name)).Value);
            if (await _userManager.IsInRoleAsync(user, "root"))
            {
                try
                {
                    var report = await _context.Reports.Where(x => x.Id == id).FirstOrDefaultAsync();
                    _context.Reports.Remove(report);
                    await _context.SaveChangesAsync();
                    return Ok();
                }
                catch (Exception)
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
        [HttpDelete("Comments/Report/Remove/Comment/{id?}")]
        public async Task<ActionResult> RemoveComment([FromRoute] string id)
        {
            var user = await _userManager.FindByNameAsync(User.Claims.FirstOrDefault(x => x.Type.Equals(ClaimTypes.Name)).Value);
            if (await _userManager.IsInRoleAsync(user, "root"))
            {
                try
                {
                    var ReportedComment = await _context.Comments.Where(x => x.Id == id).FirstOrDefaultAsync();
                    _context.Comments.Remove(ReportedComment);
                    _context.Entry(ReportedComment).State = EntityState.Deleted;
                    var reports = await _context.Reports.Where(x => x.Comment.Id == id).ToListAsync();
                    foreach (var report in reports)
                    {
                        _context.Reports.Remove(report);
                    }
                    await _context.SaveChangesAsync();
                    return Ok();
                }
                catch (Exception)
                {
                    return StatusCode(500);
                }
            }
            else
            {
                return Unauthorized();
            }
        }
    }
}
