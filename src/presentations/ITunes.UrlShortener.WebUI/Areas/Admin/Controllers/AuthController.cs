using System.Collections.Generic;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using ITunes.UrlShortener.Entities.RequestModel.Admin;
using ITunes.UrlShortener.Entities.ResponseModel;
using ITunes.UrlShortener.Entities.ResponseModel.Admin.Login;
using ITunes.UrlShortener.WebUI.Helper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace ITunes.UrlShortener.WebUI.Areas.Admin.Controllers
{
    [Area("admin")]
    public class AuthController : Controller
    {
        private readonly string _apiUrl;

        private readonly HttpClient _httpClient;
        private readonly ISecurityTokenHandler _securityTokenHandler;

        public AuthController(HttpClient httpClient, ISecurityTokenHandler securityTokenHandler,
            IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiUrl = configuration["ApiUrl"];
            _securityTokenHandler = securityTokenHandler;
        }

        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        public async Task<IActionResult> Login(LoginRequestDto requestDto)
        {
            var apiResult =
                await new ApiHelper<ApplicationResponse<LoginResponseDto>>(_httpClient, _securityTokenHandler)
                    .Post(ApiUrl.Login(_apiUrl), requestDto, false);

            if (apiResult is { State: ResponseState.Success })
            {
                var userClaims = new List<Claim>
                {
                    new(ClaimTypes.NameIdentifier, apiResult.Data.Id),
                    new(ClaimTypes.Name, apiResult.Data.Name),
                    new(ClaimTypes.Surname, apiResult.Data.Surname),
                    new(ClaimTypes.Email, apiResult.Data.Mail),
                    new("Company", apiResult.Data.Company),
                    new("Token", apiResult.Data.AccessToken)
                };

                var userIdentity = new ClaimsIdentity(userClaims);
                var claimsIdentity = new ClaimsIdentity(userClaims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties { IsPersistent = requestDto.IsRemember };

                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity), authProperties);

                return Json(new ApplicationResponse<bool>(true));
            }

            return Json(apiResult);
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            var apiResult = await
                new ApiHelper<ApplicationResponse<bool>>(_httpClient, _securityTokenHandler)
                    .Get(ApiUrl.Logout(_apiUrl));

            if (apiResult is { State: ResponseState.Success })
            {
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                return Redirect("/admin/auth/login");
            }

            return Redirect("/");
        }
    }
}