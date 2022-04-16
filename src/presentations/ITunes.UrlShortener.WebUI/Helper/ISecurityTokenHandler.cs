using System;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;

namespace ITunes.UrlShortener.WebUI.Helper
{
    public interface ISecurityTokenHandler
    {
        Task<string> GetToken();

        Task<SessionUser> GetSessionUser();
        Task CheckUnauthorizeStatus(HttpStatusCode responseStatusCode);
    }


    public class SecurityTokenHandler : ISecurityTokenHandler
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SecurityTokenHandler(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<string> GetToken()
        {
            var token = string.Empty;

            var principal = _httpContextAccessor.HttpContext?.User;

            if (null != principal)
            {
                foreach (var claim in principal.Claims)
                {
                    if (claim.Type == "Token")
                    {
                        token = claim.Value;
                    }
                }
            }

            return token;
        }

        public async Task<SessionUser> GetSessionUser()
        {
            var sessionUser = new SessionUser();
            var principal = _httpContextAccessor.HttpContext?.User;
            var claims = principal?.Identities.First().Claims.ToList();

            sessionUser.Id = claims?.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            sessionUser.Name = claims?.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;
            sessionUser.Surname = claims?.FirstOrDefault(x => x.Type == ClaimTypes.Surname)?.Value;
            sessionUser.Mail = claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
            sessionUser.Company = claims?.FirstOrDefault(x => x.Type == "Company")?.Value;
            sessionUser.AccessToken = claims?.FirstOrDefault(x => x.Type == "Token")?.Value;
            
            return sessionUser;
        }

        public async Task CheckUnauthorizeStatus(HttpStatusCode httpStatusCode)
        {
            if (httpStatusCode == HttpStatusCode.Unauthorized)
                await Microsoft.AspNetCore.Authentication.AuthenticationHttpContextExtensions.SignOutAsync(
                    _httpContextAccessor.HttpContext, CookieAuthenticationDefaults.AuthenticationScheme);
        }
    }


    public class SessionUser
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Company { get; set; }
        public string? Mail { get; set; }
        public string? AccessToken { get; set; }
    }
}