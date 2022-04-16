namespace ITunes.UrlShortener.Entities.ResponseModel.Admin.Login
{
    public class LoginResponseDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Mail { get; set; }
        public string Surname { get; set; }
        public string Company { get; set; }
        public string AccessToken { get; set; }
    }
}