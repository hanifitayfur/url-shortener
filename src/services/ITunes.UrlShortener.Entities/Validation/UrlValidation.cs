using System;
using System.ComponentModel.DataAnnotations;

namespace ITunes.UrlShortener.Entities.Validation
{
    public class UrlValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object url, ValidationContext validationContext)
        {
            var result = Uri.TryCreate(url.ToString(), UriKind.Absolute, out var uriResult)
                         && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);

            return result ? ValidationResult.Success : new ValidationResult("Url hatalı");
        }
    }
}
