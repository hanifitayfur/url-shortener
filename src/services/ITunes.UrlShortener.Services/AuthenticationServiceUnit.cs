using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ITunes.UrlShortener.Entities.RequestModel.Admin;
using ITunes.UrlShortener.Entities.ResponseModel;
using ITunes.UrlShortener.Entities.ResponseModel.Admin.Login;
using ITunes.UrlShortener.Repositories.Abstraction;
using ITunes.UrlShortener.Services.Abstraction;
using ITunes.UrlShortener.Services.Helper;
using ltunes.Core.Auth.Abstraction;
using ltunes.Security.Tools;
using Microsoft.AspNetCore.Http;

namespace ITunes.UrlShortener.Services
{
    public class AuthenticationServiceUnit : IAuthenticationServiceUnit
    {
        private readonly ITokenManager _tokenManager;
        private readonly IJwtAuthManager _jwtAuthManager;
        private readonly IUserRepository _userRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthenticationServiceUnit(
            ITokenManager tokenManager, 
            IUserRepository userRepository, 
            IJwtAuthManager jwtAuthManager,
            IHttpContextAccessor httpContextAccessor)
        {
            _tokenManager = tokenManager;
            _userRepository = userRepository;
            _jwtAuthManager = jwtAuthManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ApplicationResponse<bool>> Logout()
        {
            var accessToken = _httpContextAccessor.HttpContext?.Request.Headers["Authorization"].FirstOrDefault()
                ?.Split(" ")
                .Last();

            await _tokenManager.RemoveToken(accessToken);
            return new ApplicationResponse<bool>(true);
        }

        public async Task<ApplicationResponse<LoginResponseDto>> Authenticate(LoginRequestDto authenticateRequest)
        {
            var userByMail = await _userRepository.GetAsync(x => x.EMail == authenticateRequest.EMail);

            if (userByMail is null)
            {
                return new ApplicationResponse<LoginResponseDto>(ResponseState.Error,
                    ResponseMessage.Error.UserNotFound);
            }

            var hashPassword = SecurityHelper.HashPassword(authenticateRequest.Password, userByMail.PasswordSalt);

            var user = await _userRepository.GetAsync(x =>
                x.EMail == authenticateRequest.EMail && x.Password == hashPassword);

            if (user is null)
            {
                return new ApplicationResponse<LoginResponseDto>(ResponseState.Error,
                    ResponseMessage.Error.UserNotFound);
            }

            var claims = new List<Claim>
            {
                new("id", user.Id),
                new("email", user.EMail),
            };

            var jwtResult = _jwtAuthManager.GenerateTokens(user.EMail, claims.ToArray());
            await _tokenManager.AssignToken(jwtResult.AccessToken, jwtResult.RefreshToken);

            return new ApplicationResponse<LoginResponseDto>(new LoginResponseDto
            {
                Id = user.Id,
                Name = user.Name,
                Surname = user.Surname,
                Mail = user.EMail,
                Company = user.Company?.Name,
                AccessToken = jwtResult.AccessToken,
            });
        }
    }
}