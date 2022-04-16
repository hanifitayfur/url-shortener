using System.Collections.Generic;
using System.Threading.Tasks;
using ITunes.UrlShortener.Entities.RequestModel;
using ITunes.UrlShortener.Entities.ResponseModel;

namespace ITunes.UrlShortener.Services.Abstraction
{
    public interface IShortUrlService
    {
        Task<ApplicationResponse<ShortUrlResponseDto>> Get(string shorturl);

        Task<ApplicationResponse<List<ShortUrlResponseDto>>> List();

        Task<ApplicationResponse<ShortUrlResponseDto>> Add(ShortUrlRequestDto shortUrlRequestDto);
    }
}