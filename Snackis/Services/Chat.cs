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
    public class Chat : IChats
    {

        public async Task<bool> CreateMessages(string id, string message, string token)
        {
            string url = @"https://localhost:44364/chat/message/create";
            using (HttpClient client = new HttpClient())
            {
                var values = new Dictionary<string, string>
                {
                    {"userId", id },
                    {"message", message },
                };
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                var json = JsonConvert.SerializeObject(values, Formatting.Indented);
                var stringContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                var request = new HttpRequestMessage(HttpMethod.Post, url);
                request.Content = stringContent;

                var response = await client.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    return (true);
                }
                else
                {
                    return (false);
                }
            }
        }

        public async Task<ChatModel> GetChatByUserId(string id, string token)
        {
            var UserId = id;
            using (HttpClient client = new HttpClient())
            {
                var url = $"https://localhost:44364/chat/get/{UserId}";

                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

                var response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var chat = await response.Content.ReadFromJsonAsync<ChatModel>();
                    return chat;
                }
                else
                {
                    return new ChatModel();
                }
            }
        }

        public async Task<List<ChatsModel>> GetChats(string token)
        {
            using (HttpClient client = new HttpClient())
            {
                var url = $"https://localhost:44364/chat/get/all";

                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

                var response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var chat = await response.Content.ReadFromJsonAsync<List<ChatsModel>>();
                    return chat;
                }
                else
                {
                    return new List<ChatsModel>();
                }
            }
        }
    }
}