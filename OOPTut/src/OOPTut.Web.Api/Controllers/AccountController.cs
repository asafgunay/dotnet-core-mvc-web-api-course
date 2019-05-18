using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using OOPTut.Core.Users;
using OOPTut.Web.Api.Models;

namespace OOPTut.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration configuration;
        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            this.configuration = configuration;
        }
        // register metodu
        // login/Token metodu
        // login olur ve token olusturur
        [HttpPost]
        [Route("token")]
        public async Task<ActionResult> Login(LoginModel input)
        {
            if (ModelState.IsValid)
            {
                var loginResult = await _signInManager.PasswordSignInAsync(input.Username, input.Password, true, false);
                if (!loginResult.Succeeded)
                {
                    return BadRequest();
                }
                var user = await _userManager.FindByNameAsync(input.Username);
                return Ok(GetTokenResponse(user));
            }
            return BadRequest(ModelState);
        }
        private string GetToken(ApplicationUser user)
        {
            var utcNow = DateTime.UtcNow;

            var claims = new Claim[]
            {
                        new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                        new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, utcNow.ToString())
            };

            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.configuration.GetValue<string>("Tokens:Key")));
            var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
            var jwt = new JwtSecurityToken(
                signingCredentials: signingCredentials,
                claims: claims,
                notBefore: utcNow,
                expires: utcNow.AddSeconds(this.configuration.GetValue<int>("Tokens:Lifetime")),
                audience: this.configuration.GetValue<string>("Tokens:Audience"),
                issuer: this.configuration.GetValue<string>("Tokens:Issuer")
                );

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }

        private JwtTokenResult GetTokenResponse(ApplicationUser user)
        {
            var token = GetToken(user);
            JwtTokenResult result = new JwtTokenResult
            {
                AccessToken = token,
                ExpireInSeconds = this.configuration.GetValue<int>("Tokens:Lifetime"),
                UserId = user.Id
            };
            return result;
        }
    }
}