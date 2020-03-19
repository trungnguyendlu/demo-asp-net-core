using Demo.Infrastructure.Utils;
using Newtonsoft.Json.Linq;
using System;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Http;

namespace Demo.Infrastructure.Attribute
{
    public class ReCaptchaValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (string.IsNullOrEmpty(Common.Settings.ReCaptchaSecretKey))
            {
                return ValidationResult.Success;
            }

            var errorResult = new Lazy<ValidationResult>(() => new ValidationResult("reCAPTCHA không hợp lệ", new String[] { validationContext.MemberName }));

            if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
            {
                return errorResult.Value;
            }

            var reCaptchResponse = value.ToString();
            var reCaptchaSecret = Common.Settings.ReCaptchaSecretKey;
            
            var httpClient = new HttpClient();
            var httpResponse = httpClient.GetAsync($"https://www.google.com/recaptcha/api/siteverify?secret={reCaptchaSecret}&response={reCaptchResponse}").Result;
            if (httpResponse.StatusCode != HttpStatusCode.OK)
            {
                return errorResult.Value;
            }

            var jsonResponse = httpResponse.Content.ReadAsStringAsync().Result;
            dynamic jsonData = JObject.Parse(jsonResponse);
            if (jsonData.success != true.ToString().ToLowerInvariant())
            {
                return errorResult.Value;
            }

            return ValidationResult.Success;
        }
    }
}
