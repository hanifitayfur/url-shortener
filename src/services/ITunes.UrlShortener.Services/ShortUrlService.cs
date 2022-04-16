using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using ITunes.UrlShortener.Entities.Entities.MongoDB;
using ITunes.UrlShortener.Entities.Maps;
using ITunes.UrlShortener.Entities.RequestModel;
using ITunes.UrlShortener.Entities.ResponseModel;
using ITunes.UrlShortener.Repositories.Abstraction;
using ITunes.UrlShortener.Services.Abstraction;
using ltunes.Core.Auth.Abstraction;
using Microsoft.AspNetCore.Http;
using MongoDB.Bson;

namespace ITunes.UrlShortener.Services
{
    public class ShortUrlService : BaseServiceUnit, IShortUrlService
    {
        private readonly IShortUrlRepository _shortUrlRepository;

        public ShortUrlService(
            IUserRepository userRepository,
            IJwtAuthManager jwtAuthManager,
            IShortUrlRepository shortUrlRepository,
            IHttpContextAccessor httpContextAccessor
        ) : base(jwtAuthManager, httpContextAccessor, userRepository)
        {
            _shortUrlRepository = shortUrlRepository;
        }

        public async Task<ApplicationResponse<List<ShortUrlResponseDto>>> List()
        {
            var currentUser = await GetCurrentUser();
            var shortUrlList = _shortUrlRepository
                .Get(x => x.User.Company.Id == currentUser.Company.Id)
                .OrderByDescending(o => o.CreatedAt)
                .ToList();

            var responseList = shortUrlList.Select(UrlShortMapper.Map).ToList();

            return new ApplicationResponse<List<ShortUrlResponseDto>>(responseList);
        }

        public async Task<ApplicationResponse<ShortUrlResponseDto>> Add(ShortUrlRequestDto shortUrlRequestDto)
        {
            var currentUser = await GetCurrentUser();
            var mongoShortUrlEntity = new ShortUrl
            {
                Id = ObjectId.GenerateNewId().ToString(),
                LongUrlValue = WebUtility.UrlEncode(shortUrlRequestDto.LongURL),
                ShortUrlValue = await GenerateShortUrl(),
                User = currentUser,
                ExpireDate = shortUrlRequestDto.ExpireDate
            };

            await _shortUrlRepository.AddAsync(mongoShortUrlEntity);

            var mapObject = UrlShortMapper.Map(mongoShortUrlEntity);

            return new ApplicationResponse<ShortUrlResponseDto>(mapObject);
        }

        public async Task<ApplicationResponse<ShortUrlResponseDto>> Get(string shortUrl)
        {
            var encodeUrl = WebUtility.UrlEncode(shortUrl);

            var entityShortUrl = await _shortUrlRepository.GetAsync(url => url.ShortUrlValue.Equals(encodeUrl));
            if (entityShortUrl.ExpireDate <= DateTime.Now)
            {
                return new ApplicationResponse<ShortUrlResponseDto>(ResponseState.Error, "Url is expired");
            }

            var result = UrlShortMapper.Map(entityShortUrl);

            return new ApplicationResponse<ShortUrlResponseDto>(result);
        }

        private async Task<string> GenerateShortUrl()
        {
            var generateShortUrl = ShortUrlGenerator.Generate();
            var isValid = await _shortUrlRepository.GetAsync(url => url.ShortUrlValue.Equals(generateShortUrl));

            if (isValid != null)
            {
                await GenerateShortUrl();
            }

            return generateShortUrl;
        }
    }
}