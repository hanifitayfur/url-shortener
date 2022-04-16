namespace ITunes.UrlShortener.Services.Helper
{
    public class ResponseMessage
    {
        public class Error
        {
            public static string GeneralError = "Hata oluştu.";
            public static string UserNotFound = "Kullanıcı bulunamadı";
            public static string CompanyNotFound = "Tanımlı firma bulunamadı";
            public static string ValidUser = "Kullanıcının daha önce kaydı bulunmaktadr";
            public static string ValidCompany = "Firmanın daha önce kaydı bulunmaktadr";
        }

        public class Success
        {
            public static string Successful = "İşlem Başarılı";
        }
    }
}