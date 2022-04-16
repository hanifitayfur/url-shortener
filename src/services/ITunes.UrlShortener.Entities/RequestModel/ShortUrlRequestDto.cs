using System;
using System.ComponentModel.DataAnnotations;
using ITunes.UrlShortener.Entities.Validation;

namespace ITunes.UrlShortener.Entities.RequestModel
{
    public class ShortUrlRequestDto
    {
        [Required]
        [UrlValidation(ErrorMessage = "Url hatalı")]
        public string LongURL { get; set; }

        public DateTime ExpireDate { get; set; }
    }
}