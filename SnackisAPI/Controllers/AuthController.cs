using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SnackisAPI.Data;
using SnackisAPI.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SnackisAPI.Controllers
{
    public class AuthController : Controller
    {
        private UserManager<User> _userManager;

        private SignInManager<User> _signInManager;
        private IConfiguration _configuration;

        public AuthController(UserManager<User> userManager, SignInManager<User> signInManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody] RegisterModel model)
        {
            User newUser = new User
            {
                Email = model.Email,
                Id = Guid.NewGuid().ToString(),
                UserName = model.Username

            };
            var result = await _userManager.CreateAsync(newUser, model.Password);

            if (result.Succeeded)
            {
                User user = await _userManager.FindByNameAsync(newUser.UserName);
                if (user != null)
                {
                    await _userManager.AddToRoleAsync(user, "user");
                    return Ok(new { result = $"User {user.UserName} has been created" });
                }
                else
                {
                    return BadRequest(new { result = "Registrasion failed for unknown reasons" });
                }
            }
            else
            {
                StringBuilder stringBuilder = new StringBuilder();
                var errors = result.Errors.ToList();
                foreach (var error in errors)
                {
                    stringBuilder.Append($"{error.Description} ");
                }
                return BadRequest(new { result = stringBuilder.ToString() });
            }
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] LoginModel model)
        {
            User user = model.User.Contains("@") ? await _userManager.FindByEmailAsync(model.User) : await _userManager.FindByNameAsync(model.User);

            if (user != null)
            {
                var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);

                if (result.Succeeded)
                {
                    var userRoles = await _userManager.GetRolesAsync(user);
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var key = Encoding.ASCII.GetBytes(_configuration["key"]);

                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(new Claim[]
                       {
                            new Claim(ClaimTypes.Name, user.UserName),
                            new Claim(ClaimTypes.Email, user.Email),
                            new Claim(ClaimTypes.Role, userRoles.First().ToString())

                       }),
                        Expires = DateTime.UtcNow.AddDays(1),
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                    };

                    var token = tokenHandler.CreateToken(tokenDescriptor);
                    var tokenString = tokenHandler.WriteToken(token); 

                    return Ok(new {token = tokenString });
                }
                else
                {
                    return BadRequest();
                }
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
