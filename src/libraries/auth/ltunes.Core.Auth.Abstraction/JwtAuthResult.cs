using System;

namespace ltunes.Core.Auth.Abstraction
{
    public class JwtAuthResult
    {
        public string AccessToken { get; set; }

        public RefreshToken RefreshToken { get; set; }
    }


    public class RefreshToken
    {
        public string TokenString { get; set; }

        public DateTime ExpireAt { get; set; }
        public string UserName { get; set; }
    }
}