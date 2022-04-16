using System;
using System.Linq;
using System.Threading.Tasks;
using ITunes.UrlShortener.Entities.Entities.MongoDB;
using ITunes.UrlShortener.Repositories.Abstraction;
using ltunes.Core.Auth.Abstraction;
using Microsoft.AspNetCore.Http;

namespace ITunes.UrlShortener.Services
{
    public class BaseServiceUnit
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtAuthManager _jwtAuthManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        protected BaseServiceUnit(
            IJwtAuthManager jwtAuthManager,
            IHttpContextAccessor httpContextAccessor, IUserRepository userRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _userRepository = userRepository;
            _jwtAuthManager = jwtAuthManager;
        }

        protected string AccessToken
        {
            get
            {
                var accessToken = _httpContextAccessor.HttpContext?.Request.Headers["Authorization"].FirstOrDefault()
                    ?.Split(" ")
                    .Last();

                return accessToken;
            }
        }

        protected async Task<User> GetCurrentUser()
        {
            var decodeJtw = _jwtAuthManager.DecodeJwtToken(AccessToken);
            var id = decodeJtw.Item1.Claims.FirstOrDefault(x => x.Type == "id")?.Value;
            var user = await _userRepository.GetAsync(x => x.Id == id);

            if (user is null)
            {
                throw new Exception("User bulunamadı");
            }

            return user;
        }
    }
}