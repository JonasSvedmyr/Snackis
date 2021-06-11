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
    public class ChatsModel : PageModel
    {
        [BindProperty]
        public List<Snackis.Models.ChatsModel> Chats { get; set; }
        private readonly IChats _chats;

        public ChatsModel(IChats chats)
        {
            _chats = chats;
        }
        public async Task<ActionResult> OnGetAsync()
        {
            if (HttpContext.Session.GetString("_token") != null)
            {
                Chats = await _chats.GetChats(HttpContext.Session.GetString("_token"));
                return Page();
            }
            else
            {
                return RedirectToPage("/auth/login");
            }
        }
    }
}
