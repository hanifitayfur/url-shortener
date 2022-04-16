using System.Threading.Tasks;
using ltunes.Core.Auth.Abstraction;
using ltunes.Core.Cache.Abstraction;

namespace ltunes.Core.Auth.Jwt
{
    public class TokenManager : ITokenManager
    {
        private readonly ICacheManager _cacheManager;

        public TokenManager(ICacheManager cacheManager)
        {
            _cacheManager = cacheManager;
        }

        public Task<bool> IsCurrentActiveToken(string token)
        {
            var isSet = _cacheManager.IsSet(token);
            return Task.FromResult(isSet);
        }

        public async Task<bool> AssignToken(string token, RefreshToken refreshToken)
        {
            _cacheManager.Set(token, refreshToken, 15);

            return await Task.FromResult(true);
        }

        public Task<bool> RemoveToken(string token)
        {
            if (_cacheManager.IsSet(token))
                _cacheManager.Remove(token);

            return Task.FromResult(true);
        }
    }
}