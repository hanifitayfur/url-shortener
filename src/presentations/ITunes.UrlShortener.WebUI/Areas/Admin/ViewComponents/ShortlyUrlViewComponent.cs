using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using ITunes.UrlShortener.Entities.ResponseModel;
using ITunes.UrlShortener.WebUI.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace ITunes.UrlShortener.WebUI.Areas.Admin.ViewComponents
{
    [ViewComponent(Name = "ShortlyUrls")]
    public class ShortlyUrlViewComponent : ViewComponent
    {
        private readonly string _apiUrl;
        private readonly HttpClient _httpClient;
        private readonly ISecurityTokenHandler _securityTokenHandler;

        public ShortlyUrlViewComponent(HttpClient httpClient, ISecurityTokenHandler securityTokenHandler,
            IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiUrl = configuration["ApiUrl"];
            _securityTokenHandler = securityTokenHandler;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            try
            {
                var apiResult = await
                                    new ApiHelper<ApplicationResponse<IList<ShortUrlResponseDto>>>(_httpClient,
                                            _securityTokenHandler)
                                        .Get(ApiUrl.GetShortlyList(_apiUrl))
                                ??
                                new ApplicationResponse<IList<ShortUrlResponseDto>>();

                return View(apiResult);
            }
            catch (Exception e)
            {
                Console.Write(e);
                return View(new ApplicationResponse<IList<ShortUrlResponseDto>>(ResponseState.PageError,
                    "Bilinmeyen bir hata oluştu."));
            }
        }
    }
}