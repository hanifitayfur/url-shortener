namespace ITunes.UrlShortener.Entities.RequestModel.User
{
    public class AddUserDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string EMail { get; set; }
        public string Password { get; set; }
        public string CompanyId { get; set; }
    }
}