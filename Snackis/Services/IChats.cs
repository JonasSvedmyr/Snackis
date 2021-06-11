using Snackis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Snackis.Services
{
    public interface IChats
    {
        Task<ChatModel> GetChatByUserId(string id, string token);
        Task<List<ChatsModel>> GetChats(string token);
        Task<bool> CreateMessages(string id, string message, string token);
    }
 }
