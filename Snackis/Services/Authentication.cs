
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Snackis.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;


namespace Snackis.Services
{
    public class ResultModel
    {
        public string result { get; set; }
    }
    public class Token
    {
        public string token { get; set; }
    }

    public class Authentication : IAuthentication
    {
        private readonly IConfiguration _configuration;

        public Authentication(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<string> GetUser(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var _token = handler.ReadJwtToken(token).Payload;
            return _token["unique_name"].ToString();
        }

        public async Task<bool> IsAdmin(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var _token = handler.ReadJwtToken(token).Payload;
            if (_token["role"].ToString() == "root")
            {
                return true;
            }
            return false;
        }

        public async Task<(bool, string)> Loggin(string user, string password)
        {
            var url = _configuration["BaseApiString"];
            url += "/login";

            using (HttpClient client = new HttpClient())
            {
                var values = new Dictionary<string, string>
                {
                    {"user", user },
                    {"password", password }
                };

                var json = JsonConvert.SerializeObject(values, Formatting.Indented);
                var stringContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                var request = new HttpRequestMessage(HttpMethod.Post, url);
                request.Content = stringContent;

                var response = await client.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    var token = await response.Content.ReadFromJsonAsync<Token>();
                    return (true, token.token);
                }
                else
                {
                    return (false, "Unable to login");
                }
            }
        }

        public async Task<(bool, string)> Register(string username, string email, string password)
        {
            var url = _configuration["BaseApiString"];
            url += "/register";

            using (HttpClient client = new HttpClient())
            {
                var values = new Dictionary<string, string>
                {
                    {"username", username },
                    {"email", email },
                    {"password", password }
                };

                var json = JsonConvert.SerializeObject(values, Formatting.Indented);
                var stringContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                var request = new HttpRequestMessage(HttpMethod.Post, url);
                request.Content = stringContent;

                var response = await client.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    var token = await response.Content.ReadFromJsonAsync<ResultModel>();
                    return (true, "Success");
                }
                else
                {
                    if (response.StatusCode != System.Net.HttpStatusCode.NotFound)
                    {
                        var errors = await response.Content.ReadFromJsonAsync<ResultModel>();
                        var error = errors.result.Split('.').FirstOrDefault().Trim();
                        return (false, error);
                    }
                    return (false, "Unable to register");
                }
            }
        }
    }
}
