﻿using Microsoft.Extensions.Configuration;
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
    public class Comments : IComments
    {
        private readonly IConfiguration _configuration;

        public Comments(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<(string, bool)> CreateComment(string comment, string postid, string token)
        {
            var url = _configuration["BaseApiString"];
            url += "/comments/create";
            using (HttpClient client = new HttpClient())
            {
                var values = new Dictionary<string, string>
                {
                    {"PostId", postid },
                    {"Comment", comment },

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

        public async Task<(string, bool)> CreateReport(string reason, string id, string token)
        {
            var url = _configuration["BaseApiString"];
            url += "/comments/Report/create";
            using (HttpClient client = new HttpClient())
            {
                var values = new Dictionary<string, string>
                {
                    {"commentId", id },
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

        public async Task<(string, bool)> DeleteComment(string id, string token)
        {
            var url = _configuration["BaseApiString"];
            url += $"/comments/delete/{id}";
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

        public async Task<(string, bool)> EditComment(string comment, string id, string token)
        {
            var url = _configuration["BaseApiString"];
            url += "/comments/edit";
            using (HttpClient client = new HttpClient())
            {
                var values = new Dictionary<string, string>
                {
                    {"CommentId", id },
                    {"Comment", comment },

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

        public async Task<CommentsModel> GetComment(string id)
        {
            using (HttpClient client = new HttpClient())
            {
                var url = _configuration["BaseApiString"];
                url += $"/comments/get/{id}";

                var response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var comment = await response.Content.ReadFromJsonAsync<CommentsModel>();
                    return comment;
                }
                else
                {
                    return new CommentsModel();
                }
            }
        }

        public async Task<List<CommentsModel>> GetComments(string postid)
        {
            using (HttpClient client = new HttpClient())
            {
                var url = _configuration["BaseApiString"];
                url += $"/comments/get/all/{postid}";

                var response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var comments = await response.Content.ReadFromJsonAsync<List<CommentsModel>>();
                    return comments;
                }
                else
                {
                    return new List<CommentsModel>();
                }
            }
        }

        public async Task<ReportCommentModel> GetReportedComment(string id,string token)
        {
            using (HttpClient client = new HttpClient())
            {
                var url = _configuration["BaseApiString"];
                url += $"/comments/report/get/{id}";
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                var response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var comment = await response.Content.ReadFromJsonAsync<ReportCommentModel>();
                    return comment;
                }
                else
                {
                    return new ReportCommentModel();
                }
            }
        }

        public async Task<List<ReportCommentModel>> GetReportedComments(string token)
        {
            using (HttpClient client = new HttpClient())
            {
                var url = _configuration["BaseApiString"];
                url += "/comments/report/get/all";
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                var response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var comments = await response.Content.ReadFromJsonAsync<List<ReportCommentModel>>();
                    return comments;
                }
                else
                {
                    return new List<ReportCommentModel>();
                }
            }
        }

        public async Task<(string, bool)> RemoveReport(string id, string token)
        {
            var url = _configuration["BaseApiString"];
            url += $"/comments/report/remove/{id}";
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

        public async Task<(string, bool)> RemoveReportedComment(string id, string token)
        {
            var url = _configuration["BaseApiString"];
            url += $"/comments/report/remove/comment/{id}";
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
    }
}
