using System;
using System.Text;
using ltunes.Core.Auth.Abstraction;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;


namespace ltunes.Core.Auth.Jwt
{
    public static class JwtAuthenticationMiddlewareExtensions
    {
        public static void AddJwtAuthenticationService(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = true;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["JWTSettings:Issuer"],
                    ValidAudience = configuration["JWTSettings:Audience"],
                    ClockSkew = TimeSpan.FromMinutes(int.Parse(configuration["JWTSettings:TokenExpireMinute"])),
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration["JWTSettings:Secret"])),
                };
            });


            services.AddTransient<IJwtAuthManager, JwtAuthManager>();
            services.AddTransient<ITokenManager, TokenManager>();
        }

    }
}