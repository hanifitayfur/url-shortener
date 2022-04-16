using System;
using Microsoft.Extensions.Configuration;

namespace ltunes.Core.Auth.Jwt
{
    public class JwtSettings
    {
        private readonly IConfiguration _configuration;

        protected JwtSettings(IConfiguration configuration)
        {
            _configuration = configuration;
            CheckJwtSettingValues();
        }

        public void CheckJwtSettingValues()
        {
            if (string.IsNullOrEmpty(_configuration["JWTSettings:Secret"]))
            {
                throw new ArgumentNullException($"JWTSettings:Secret is null");
            }

            if (string.IsNullOrEmpty(_configuration["JWTSettings:Audience"]))
            {
                throw new ArgumentNullException($"JWTSettings:Audience is null");
            }

            if (string.IsNullOrEmpty(_configuration["JWTSettings:Issuer"]))
            {
                throw new ArgumentNullException($"JWTSettings:Issuer is null");
            }
        }
    }
}