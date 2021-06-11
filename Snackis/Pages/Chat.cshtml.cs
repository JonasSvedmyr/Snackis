using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Snackis.Services;

namespace Snackis.Pages
{
    public class ChatModel : PageModel
    {
        private readonly IChats _chats;

        [BindProperty(SupportsGet = true)]
        public string Id { get; set; }
        [BindProperty(SupportsGet = true)]
        public string UserId { get; set; }
        [BindProperty]
        public string Message { get; set; }
        [BindProperty]
        public Snackis.Models.ChatModel chat { get; set; }
        public ChatModel(IChats chats)
        {
            _chats = chats;
        }
        public async Task<ActionResult> OnGetAsync()
        {
            if (HttpContext.Session.GetString("_token") != null)
            {
                chat = await _chats.GetChatByUserId(UserId, HttpContext.Session.GetString("_token"));
                return Page();
            }
            else
            {
                return RedirectToPage("/auth/login");
            }
        }
        public async Task<ActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var success = await _chats.CreateMessages(UserId, Message, HttpContext.Session.GetString("_token"));

                if (success)
                {
                    return RedirectToPage("/Chat", new { UserId = UserId });
                }
                else
                {
                    return Page();
                }
            }
            return Page();
        }
    }
}
