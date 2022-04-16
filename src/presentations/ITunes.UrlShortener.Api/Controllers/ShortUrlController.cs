using System.Net;
using System.Threading.Tasks;
using ITunes.UrlShortener.Entities.RequestModel;
using ITunes.UrlShortener.Entities.ResponseModel;
using ITunes.UrlShortener.Services.Abstraction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ITunes.UrlShortener.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShortUrlController : ControllerBase
    {
        private readonly IShortUrlService _shortUrlService;

        public ShortUrlController(IShortUrlService shortUrlService)
        {
            _shortUrlService = shortUrlService;
        }

        [HttpPost]
        [Authorize]
        [Route("create")]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(object), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Create([FromBody] ShortUrlRequestDto shortUrlRequestDto)
        {
            if (ModelState.IsValid)
            {
                var result = await _shortUrlService.Add(shortUrlRequestDto);
                return Ok(result);
            }

            return BadRequest(ModelState.Values);
        }

        [HttpGet]
        [Authorize]
        [Route("list")]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(object), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> ListAll()
        {
            var shortUrls = await _shortUrlService.List();
            return Ok(shortUrls);
        }

        [HttpGet]
        [Route("get")]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(object), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Get(string shortUrl)
        {
            if (string.IsNullOrEmpty(shortUrl))
                return NotFound();

            ApplicationResponse<ShortUrlResponseDto> urlItem = await _shortUrlService.Get(shortUrl);

            if (urlItem == null)
                return NotFound();

            return Ok(urlItem);
        }
    }
}