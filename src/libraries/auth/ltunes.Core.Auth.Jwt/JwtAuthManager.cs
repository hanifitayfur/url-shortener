using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using ltunes.Core.Auth.Abstraction;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;


namespace ltunes.Core.Auth.Jwt
{
    public class JwtAuthManager : JwtSettings, IJwtAuthManager
    {
        private readonly IConfiguration _configuration;


        public JwtAuthManager(IConfiguration configuration) : base(configuration)
        {
            _configuration = configuration;
        }

        public JwtAuthResult GenerateTokens(string username, Claim[] claims)
        {
            var jwtToken = new JwtSecurityToken(
                _configuration["JWTSettings:Issuer"],
                _configuration["JWTSettings:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(int.Parse(_configuration["JWTSettings:TokenExpireMinute"])),
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["JWTSettings:Secret"])),
                    SecurityAlgorithms.HmacSha256Signature));

            var accessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);

            var refreshToken = new RefreshToken
            {
                UserName = username,
                TokenString = GenerateRefreshTokenString(),
                ExpireAt = DateTime.Now.AddMinutes(2)
            };

            return new JwtAuthResult
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
        }

        public JwtAuthResult Refresh(string accessToken)
        {
            var (principal, jwtToken) = DecodeJwtToken(accessToken);
            if (jwtToken == null || !jwtToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256Signature))
            {
                throw new SecurityTokenException("Invalid token");
            }

            var userName = principal.Identity?.Name;
            return GenerateTokens(userName, principal.Claims.ToArray()); // need to recover the original claims
        }

        public (ClaimsPrincipal, JwtSecurityToken) DecodeJwtToken(string token)
        {
            if (string.IsNullOrWhiteSpace(token))
            {
                throw new SecurityTokenException("Invalid token");
            }

            var principal = new JwtSecurityTokenHandler()
                .ValidateToken(token,
                    new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = _configuration["JWTSettings:Issuer"],
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey =
                            new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["JWTSettings:Secret"])),
                        ValidAudience = _configuration["JWTSettings:Audience"],
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.FromMinutes(1)
                    },
                    out var validatedToken);
            return (principal, validatedToken as JwtSecurityToken);
        }

        private static string GenerateRefreshTokenString()
        {
            var randomNumber = new byte[32];
            using var randomNumberGenerator = RandomNumberGenerator.Create();
            randomNumberGenerator.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
    }
}