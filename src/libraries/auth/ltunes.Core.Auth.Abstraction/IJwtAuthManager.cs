using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ltunes.Core.Auth.Abstraction
{
    public interface IJwtAuthManager
    {
        JwtAuthResult GenerateTokens(string username, Claim[] claims);
        JwtAuthResult Refresh(string accessToken);
        (ClaimsPrincipal, JwtSecurityToken) DecodeJwtToken(string token);
    }
}