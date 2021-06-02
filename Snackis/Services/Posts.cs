﻿using Newtonsoft.Json;
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
        public async Task<(string, bool)> CreatePost(string title, string description, string token, string subjectId)
        {
            string url = @"https://localhost:44364/post/create";
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
            string url = $"https://localhost:44364/post/delete/{Id}";
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
            string url = @"https://localhost:44364/post/edit";
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
                var url = $"https://localhost:44364/post/get/{id}";

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
                var url = $"https://localhost:44364/post/get/all/{subjectId}";

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
    }
}