using System.Net;
using System.Threading.Tasks;
using ITunes.UrlShortener.Entities.RequestModel.User;
using ITunes.UrlShortener.Services.Abstraction;
using Microsoft.AspNetCore.Mvc;

namespace ITunes.UrlShortener.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("add")]
        // [Authorize]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(object), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Add(AddUserDto request)
        {
            var result = await _userService.AddUser(request);
            return Ok(result);
        }
    }
}