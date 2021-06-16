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
    public class User : IUser
    {
        private readonly IConfiguration _configuration;

        public User(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<string> GetProfilePicture(string token)
        {
            using (HttpClient client = new HttpClient())
            {
                var url = _configuration["BaseApiString"];
                url += "/User/Picture/Get";
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

                var response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var ImageUrl = await response.Content.ReadFromJsonAsync<ImageUrlModel>();
                    return ImageUrl.ImageUrl;
                }
                else
                {
                    return "";
                }
            }
        }

        public async Task<bool> SetProfilePicture(string ImageUrl, string token)
        {
            var url = _configuration["BaseApiString"];
            url += "/User/Picture/Set";
            using (HttpClient client = new HttpClient())
            {
                var values = new Dictionary<string, string>
                {
                    {"imageUrl", ImageUrl },
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
