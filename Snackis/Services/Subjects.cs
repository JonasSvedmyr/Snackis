using Newtonsoft.Json;
using Snackis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Snackis.Services
{
    public class Subjects : ISubjects
    {
        public async Task<(string, bool)> CreateSubject(string title, string description, string token)
        {
            string url = @"https://localhost:44364/subject/add";
            using (HttpClient client = new HttpClient())
            {
                var values = new Dictionary<string, string>
                {
                    {"title", title },
                    {"description", description },
                };
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                var json = JsonConvert.SerializeObject(values, Formatting.Indented);
                var stringContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                var request = new HttpRequestMessage(HttpMethod.Post, url);
                request.Content = stringContent;

                var response = await client.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    return ("Success", true);
                }
                else
                {
                    return ("Unable to create", false);
                }
            }
        }

        public async Task<(string, bool)> DeleteSubject(string id, string token)
        {
            using (HttpClient client = new HttpClient())
            {
                var url = $"https://localhost:44364/subject/delete/{id}";
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

                var response = await client.DeleteAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    return ("Success", true);
                }
                else
                {
                    return ("Unable to delete", false);
                }
            }
        }

        public async Task<(string, bool)> EditSubject(string title, string description, string id, string token)
        {
            string url = @"https://localhost:44364/subject/edit";
            using (HttpClient client = new HttpClient())
            {
                var values = new Dictionary<string, string>
                {
                    {"title", title },
                    {"description", description },
                    {"id", id },
                };
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                var json = JsonConvert.SerializeObject(values, Formatting.Indented);
                var stringContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                var request = new HttpRequestMessage(HttpMethod.Post, url);
                request.Content = stringContent;

                var response = await client.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    return ("Success", true);
                }
                else
                {
                    return ("Unable to create", false);
                }
            }
        }

        public async Task<SubjectModel> GetSubject(string id)
        {
            using (HttpClient client = new HttpClient())
            {
                var url = $"https://localhost:44364/subject/get/{id}";

                var response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var subject = await response.Content.ReadFromJsonAsync<SubjectModel>();
                    return subject;
                }
                else
                {
                    return new SubjectModel();
                }
            }
        }

        public async Task<List<SubjectModel>> GetSubjects()
        {
            using (HttpClient client = new HttpClient())
            {
                var url = @"https://localhost:44364/subject/get";

                var response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var subjects = await response.Content.ReadFromJsonAsync<List<SubjectModel>>();
                    return subjects;
                }
                else
                {
                    return new List<SubjectModel>();
                }
            }
        }
    }
}
