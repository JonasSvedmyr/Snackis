using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
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
    public class ChatController : Controller
    {
        private UserManager<User> _userManager;
        private readonly Context _context;
        private IConfiguration _configuration;

        public ChatController(UserManager<User> userManager, Context context, IConfiguration configuration)
        {
            _userManager = userManager;
            _context = context;
            _configuration = configuration;
        }
        [Authorize]
        [HttpPost("Chat/Message/Create")]
        public async Task<ActionResult> CreateMessage([FromBody] CreateMessageModel model)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(User.Claims.FirstOrDefault(x => x.Type.Equals(ClaimTypes.Name)).Value);
                var user2 = await _userManager.FindByIdAsync(model.UserId);
                if (user2 == null)
                {
                    return StatusCode(500);
                }
                else if (user2.Id == user.Id)
                {
                    return BadRequest();
                }

                var chat = await _context.Chats.Include(x => x.Messages).Where(x => (x.User.Id == user2.Id && x.User2.Id == user.Id) || (x.User2.Id == user2.Id && x.User.Id == user.Id)).FirstOrDefaultAsync();

                if (chat == null)
                {
                    return StatusCode(500);
                }
                else if (chat.User.Id != user.Id && chat.User2.Id != user.Id)
                {
                    return Unauthorized();
                }
                else
                {
                    chat.Messages.Add(new Message
                    {
                        Id = Guid.NewGuid().ToString(),
                        Sent = DateTime.Now,
                        Text = model.Message,
                        UserId = user.Id,
                    });

                    await _context.SaveChangesAsync();

                }
                return Ok();
            }
            catch
            {

                return BadRequest();
            }

        }
        [Authorize]
        [HttpGet("Chat/Get/{UserId}")]
        public async Task<ActionResult> CreateChat([FromRoute] string UserId)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(User.Claims.FirstOrDefault(x => x.Type.Equals(ClaimTypes.Name)).Value);
                var user2 = await _userManager.FindByIdAsync(UserId);
                if (user2 == null)
                {
                    return StatusCode(500);
                }
                else if (user2.Id == user.Id)
                {
                    return BadRequest();
                }

                var chat = await _context.Chats.Include(x => x.Messages).Where(x => (x.User.Id == UserId && x.User2.Id == user.Id) || (x.User2.Id == UserId && x.User.Id == user.Id)).FirstOrDefaultAsync();

                if (chat != null)
                {
                    List<object> Messeges = new List<object>();

                    var _messages = chat.Messages.OrderByDescending(x => x.Sent);
                    if (chat.Messages != null)
                    {
                        foreach (var message in _messages)
                        {
                            if (message.UserId == user.Id)
                            {
                                Messeges.Add(new
                                {
                                    id = message.Id,
                                    text = message.Text,
                                    sent = message.Sent,
                                    UserId = message.UserId,
                                    Username = user.UserName
                                });
                            }
                            else
                            {
                                Messeges.Add(new
                                {
                                    id = message.Id,
                                    text = message.Text,
                                    sent = message.Sent,
                                    UserId = message.UserId,
                                    Username = user2.UserName
                                });
                            }
                        }
                    }

                    return Ok(new
                    {
                        id = chat.Id,
                        messages = Messeges
                    });
                }
                else
                {
                    var _chat = new Chat
                    {
                        Id = Guid.NewGuid().ToString(),
                        User = user,
                        User2 = user2,
                    };
                    await _context.Chats.AddAsync(_chat);
                    await _context.SaveChangesAsync();

                    List<object> Messeges = new List<object>();

                    return Ok(new
                    {
                        id = _chat.Id,
                        messages = Messeges
                    });

                }
            }
            catch
            {

                return BadRequest();
            }

        }
        [Authorize]
        [HttpGet("Chat/Get/All")]
        public async Task<ActionResult> GetChat()
        {
            try
            {
                var user = await _userManager.FindByNameAsync(User.Claims.FirstOrDefault(x => x.Type.Equals(ClaimTypes.Name)).Value);

                var chats = await _context.Chats.Include(x => x.User).Include(x => x.User2).Where(x => x.User.Id == user.Id || x.User2.Id == user.Id).ToListAsync();



                List<object> Chats = new List<object>();

                foreach (var chat in chats)
                {
                    if (user.Id != chat.User.Id)
                    {
                        Chats.Add(new
                        {
                            userid = chat.User.Id,
                            user = chat.User.UserName,
                        });
                    }
                    else
                    {
                        Chats.Add(new
                        {
                            userid = chat.User2.Id,
                            user = chat.User2.UserName,
                        });
                    }
                }
                return Ok(Chats);
            }
            catch
            {

                return BadRequest();
            }

        }

    }
}
