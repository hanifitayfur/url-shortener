namespace ITunes.UrlShortener.Entities.RequestModel.Admin
{
    public class LoginRequestDto
    {
        public string EMail { get; set; }
        public string Password { get; set; }
        public bool IsRemember { get; set; }
    }
}