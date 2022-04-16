using Microsoft.Extensions.Configuration;

namespace ITunes.UrlShortener.WebUI.Helper
{
    public class ApiUrl
    {
        public static string Login(string baseUri) => $"{baseUri}/Authenticate/login";
        public static string Logout(string baseUri) => $"{baseUri}/Authenticate/logout";
        public static string AddShortly(string baseUri) => $"{baseUri}/ShortUrl/Create";
        public static string GetShortlyList(string baseUri) => $"{baseUri}/ShortUrl/List";
        public static string GetShortly(string baseUri,string shortUrl) => $"{baseUri}/ShortUrl/get?shortUrl={shortUrl}";
    }
}