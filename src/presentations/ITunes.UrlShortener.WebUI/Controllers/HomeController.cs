using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using ITunes.UrlShortener.Entities.ResponseModel;
using ITunes.UrlShortener.WebUI.Helper;
using ITunes.UrlShortener.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace ITunes.UrlShortener.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly string _apiUrl;
        private readonly HttpClient _httpClient;
        private readonly ILogger<HomeController> _logger;
        private readonly ISecurityTokenHandler _securityTokenHandler;


        public HomeController(ILogger<HomeController> logger, HttpClient httpClient,
            ISecurityTokenHandler securityTokenHandler, IConfiguration configuration)
        {
            _logger = logger;
            _httpClient = httpClient;
            _apiUrl = configuration["ApiUrl"];
            _securityTokenHandler = securityTokenHandler;
        }


        public async Task<IActionResult> Index(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    return View();
                }

                var apiResult =
                    await new ApiHelper<ApplicationResponse<ShortUrlResponseDto>>(_httpClient, _securityTokenHandler)
                        .Get(ApiUrl.GetShortly(_apiUrl,id));

                if (apiResult is not null && apiResult.State == ResponseState.Success && apiResult.Data is not null)
                {
                    return Redirect(apiResult.Data.LongUrl);
                }

                return View();
            }
            catch (Exception e)
            {
                return View();
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}