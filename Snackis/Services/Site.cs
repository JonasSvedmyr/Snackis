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
    public class Site : ISite
    {
        public async Task<SiteModel> GetTitle()
        {
            using (HttpClient client = new HttpClient())
            {
                var url = $"https://localhost:44364/Title/Get";

                var response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var site = await response.Content.ReadFromJsonAsync<SiteModel>();
                    return site;
                }
                else
                {
                    return new SiteModel();
                }
            }
        }

        public async Task<bool> SetTitle(string Title, string token)
        {
            string url = @"https://localhost:44364/Title/Set";
            using (HttpClient client = new HttpClient())
            {
                var values = new Dictionary<string, string>
                {
                    {"title", Title }
                };
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                var json = JsonConvert.SerializeObject(values, Formatting.Indented);
                var stringContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                var request = new HttpRequestMessage(HttpMethod.Post, url);
                request.Content = stringContent;

                var response = await client.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
