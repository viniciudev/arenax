﻿using Core;
using Core.DTOs;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Infrastructure.Authenticate
{
    public class JWTManager : IJWTManager
    {
        private readonly IConfiguration iconfiguration;

        public JWTManager(IConfiguration iconfiguration)
        {
            this.iconfiguration = iconfiguration;
        }

        public async Task<TokenJWT?> Authenticate(User users)
        {
            // Else we generate JSON Web Token
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.UTF8.GetBytes(iconfiguration["JWT:Key"]!);
        
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, users.Name),
                    new Claim("UserId", users.Id.ToString()),
                }),
                Expires = DateTime.UtcNow.AddDays(100),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return new TokenJWT { Token = tokenHandler.WriteToken(token) };
        }
    }

    public interface IJWTManager
    {
        Task<TokenJWT?> Authenticate(User users);
    }
}
