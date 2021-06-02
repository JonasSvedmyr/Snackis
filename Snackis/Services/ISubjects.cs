using Snackis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Snackis.Services
{
    public interface ISubjects
    {
        Task<List<SubjectModel>> GetSubjects();
        Task<SubjectModel> GetSubject(string id);
        Task<(string, bool)> CreateSubject(string title, string description, string token);
        Task<(string, bool)> DeleteSubject(string id, string token);
        Task<(string, bool)> EditSubject(string title, string description, string id, string token);
    }
}
