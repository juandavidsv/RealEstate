using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using WebApi.Models;

/// <summary>
/// Class in charge of logging and creating the security Token
/// </summary>
namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public AccountController(
            IConfiguration configuration)
        {
            _configuration = configuration;
        }
        /// <summary>
        /// Login Api
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns>ActionResult UserToken</returns>       
        [HttpPost("Login")]
        public async Task<ActionResult<UserToken>> Login([FromBody] UserInfo userInfo)
        {
            var pass = _configuration["User:Pass"];
            var Email = _configuration["User:Email"];

            if (userInfo.Password.Equals(pass) && userInfo.Email.Equals(Email))
            {
                return BuildToken(userInfo, "Admin");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return BadRequest(ModelState);
            }
        }
        /// <summary>
        /// BuildToken security
        /// </summary>
        /// <param name="userInfo">userInfo</param>
        /// <param name="Password">Password </param>
        /// <returns></returns>
        private UserToken BuildToken(UserInfo userInfo, string Password)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.UniqueName, userInfo.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            claims.Add(new Claim(ClaimTypes.Role, Password));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expiration = DateTime.UtcNow.AddDays(1);

            JwtSecurityToken token = new JwtSecurityToken(
               issuer: null,
               audience: null,
               claims: claims,
               expires: expiration,
               signingCredentials: creds);

            return new UserToken()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expiration
            };
        }

    }
}