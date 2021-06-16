using Microsoft.Extensions.Configuration;
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
    public class Posts : IPosts
    {
        private readonly IConfiguration _configuration;

        public Posts(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<(string, bool)> CreatePost(string title, string description, string token, string subjectId)
        {
            var url = _configuration["BaseApiString"];
            url += "/post/create";
            using (HttpClient client = new HttpClient())
            {
                var values = new Dictionary<string, string>
                {
                    {"title", title },
                    {"description", description },
                    {"subjectId", subjectId },
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
                    return ( "Unable to create",false);
                }
            }
        }

        public async Task<(string, bool)> DeletePost(string Id, string token)
        {
            var url = _configuration["BaseApiString"];
            url += $"/post/delete/{Id}";
            using (HttpClient client = new HttpClient())
            {
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

        public async Task<(string, bool)> EditPost(string title, string description, string id, string token)
        {
            var url = _configuration["BaseApiString"];
            url += "/post/edit";
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
                    return ("Unable to update", false);
                }
            }
        }

        public async Task<PostModel> GetPost(string id)
        {
            using (HttpClient client = new HttpClient())
            {
                var url = _configuration["BaseApiString"];
                url += $"/post/get/{id}";

                var response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var post = await response.Content.ReadFromJsonAsync<PostModel>();
                    return post;
                }
                else
                {
                    return new PostModel();
                }
            }
        }

        public async Task<List<PostModel>> GetPosts(string subjectId)
        {
            using (HttpClient client = new HttpClient())
            {
                var url = _configuration["BaseApiString"];
                url += $"/post/get/all/{subjectId}";

                var response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var posts = await response.Content.ReadFromJsonAsync<List<PostModel>>();
                    return posts;
                }
                else
                {
                    return new List<PostModel>();
                }
            }
        }

        public async Task<ReportPostModel> GetReportedPost(string id, string token)
        {
            using (HttpClient client = new HttpClient())
            {
                var url = _configuration["BaseApiString"];
                url += $"/post/report/get/{id}";
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                var response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var posts = await response.Content.ReadFromJsonAsync<ReportPostModel>();
                    return posts;
                }
                else
                {
                    return new ReportPostModel();
                }
            }
        }

        public async Task<List<ReportPostModel>> GetReportedPosts(string token)
        {
            using (HttpClient client = new HttpClient())
            {
                var url = _configuration["BaseApiString"];
                url += "/post/report/get/all";
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                var response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var posts = await response.Content.ReadFromJsonAsync<List<ReportPostModel>>();
                    return posts;
                }
                else
                {
                    return new List<ReportPostModel>();
                }
            }
        }

        public async Task<(string, bool)> RemoveReport(string id, string token)
        {
            var url = _configuration["BaseApiString"];
            url += $"/post/report/remove/{id}";
            using (HttpClient client = new HttpClient())
            {
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

        public async Task<(string, bool)> RemoveReportedPost(string id, string token)
        {
            var url = _configuration["BaseApiString"];
            url += "/post/report/remove/post/{id}";
            using (HttpClient client = new HttpClient())
            {
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

        public async Task<(string, bool)> ReportPost(string reason, string id, string token)
        {
            var url = _configuration["BaseApiString"];
            url += "/post/Report/create";
            using (HttpClient client = new HttpClient())
            {
                var values = new Dictionary<string, string>
                {
                    {"postId", id },
                    {"reason", reason },
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


    }
}
