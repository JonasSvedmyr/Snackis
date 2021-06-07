using Snackis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Snackis.Services
{
    public interface IComments
    {
        Task<List<CommentsModel>> GetComments(string postid);
        Task<CommentsModel> GetComment(string id);
        Task<(string, bool)> CreateComment(string comment, string postid, string token);
        Task<(string, bool)> DeleteComment(string id, string token);
        Task<(string, bool)> EditComment(string comment, string id, string token);
        Task<(string, bool)> CreateReport(string reason, string id, string token);
        Task<ReportCommentModel> GetReportedComment(string id, string token);
        Task<List<ReportCommentModel>> GetReportedComments(string token);
        Task<(string, bool)> RemoveReportedComment(string id, string token);
        Task<(string, bool)> RemoveReport(string id, string token);

    }
}
