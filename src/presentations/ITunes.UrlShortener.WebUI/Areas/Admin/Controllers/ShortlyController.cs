using System.Net.Http;
using System.Threading.Tasks;
using ITunes.UrlShortener.Entities.RequestModel;
using ITunes.UrlShortener.Entities.ResponseModel;
using ITunes.UrlShortener.WebUI.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace ITunes.UrlShortener.WebUI.Areas.Admin.Controllers
{
    [Area("admin")]
    public class ShortlyController : Controller
    {
        private readonly string _apiUrl;
        private readonly HttpClient _httpClient;
        private readonly ISecurityTokenHandler _securityTokenHandler;

        public ShortlyController(HttpClient httpClient, ISecurityTokenHandler securityTokenHandler,
            IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiUrl = configuration["ApiUrl"];
            _securityTokenHandler = securityTokenHandler;
        }


        [Authorize]
        public ActionResult List() => View();

        [Authorize]
        public ActionResult Create() => View();

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(ShortUrlRequestDto requestDto)
        {
            var apiResult = await
                new ApiHelper<ApplicationResponse<ShortUrlResponseDto>>(_httpClient, _securityTokenHandler)
                    .Post(ApiUrl.AddShortly(_apiUrl), requestDto);

            return Json(apiResult);
        }
        
        [Authorize]
        public IActionResult RefreshShortlyUrlsComponent()
        {
            return ViewComponent("ShortlyUrls");
        }
    }
}