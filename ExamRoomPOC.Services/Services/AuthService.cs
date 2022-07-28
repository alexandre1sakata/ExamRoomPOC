using ExamRoomPOC.Domain.Interfaces.Services;
using ExamRoomPOC.Domain.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ExamRoomPOC.Services.Services
{
    public class AuthService : IAuthService
    {
        public IConfiguration _configuration;

        public AuthService(IConfiguration config)
        {
            _configuration = config;
        }

        public string GenerateToken(User user)
        {
            var claims = new [] 
            { 
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.Username),
                new Claim("Password", user.Password)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expiration = DateTime.UtcNow.AddHours(1);

            var securityToken = new JwtSecurityToken(
                issuer: null,
                audience: null,
                claims,
                expires: expiration,
                signingCredentials: signIn);

            var token = new JwtSecurityTokenHandler().WriteToken(securityToken);

            return token;
        }
    }
}
