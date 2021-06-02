using Snackis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Snackis.Services
{
    public interface IPosts
    {
        Task<List<PostModel>> GetPosts(string subjectId);
        Task<PostModel> GetPost(string id);
        Task<(string, bool)> CreatePost(string title, string description, string token, string subjectId);
        Task<(string, bool)> DeletePost(string Id, string token);
        Task<(string, bool)> EditPost(string title, string description, string id, string token);
    }
}
