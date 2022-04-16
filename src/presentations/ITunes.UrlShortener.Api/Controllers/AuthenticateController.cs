using System.Net;
using System.Threading.Tasks;
using ITunes.UrlShortener.Entities.RequestModel.Admin;
using ITunes.UrlShortener.Entities.ResponseModel;
using ITunes.UrlShortener.Services.Abstraction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ITunes.UrlShortener.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly IAuthenticationServiceUnit _authenticationServiceUnit;

        public AuthenticateController(IAuthenticationServiceUnit authenticationServiceUnit)
        {
            _authenticationServiceUnit = authenticationServiceUnit;
        }

        [HttpPost("login")]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(object), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Authenticate(LoginRequestDto authenticateRequest)
        {
            var response = await _authenticationServiceUnit.Authenticate(authenticateRequest);
            return Ok(response);
        }
        
        [Authorize]
        [HttpGet("logout")]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(object), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Logout()
        {
            ApplicationResponse<bool> response = await _authenticationServiceUnit.Logout();
            return Ok(response);
        }
    }
}