namespace ITunes.UrlShortener.Entities.Entities.MongoDB
{
    public class User : MongoDbEntity
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string EMail { get; set; }
        public string Password { get; set; }
        public string PasswordSalt { get; set; }

        public Company Company { get; set; }
    }
}