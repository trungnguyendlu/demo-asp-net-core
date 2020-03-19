using System;

namespace Demo.Entity
{
    public class Settings : BaseEntity
    {
        public string SiteName { get; set; }
        public int TitleTemplateType { get; set; }
        public string TitleSeparate { get; set; }
        public string Version { get; set; }
        public string Keywords { get; set; }
        public string Description { get; set; }
        public string IconUrl { get; set; }
        public int PageSize { get; set; }
        
        public string CompanyName { get; set; }
        public string Address { get; set; }
        public string EmailContact { get; set; }
        public string PhoneNumber { get; set; }

        //Social
        public string FacebookUrl { get; set; }
        public string TwitterUrl { get; set; }
        public string GooglePlusUrl { get; set; }
        public string YoutubeUrl { get; set; }
        public string LinkedInUrl { get; set; }
        public string PinterestUrl { get; set; }
        public string InstagramUrl { get; set; }


        public string GoogleConsoleVerification { get; set; }
        public string YandexVerification { get; set; }
        public string BingVerification { get; set; }

        public string GoogleAnalyticsKey { get; set; }
        public string FacebookPixelId { get; set; }
        public string ReCaptchaSiteKey { get; set; }
        public string ReCaptchaSecretKey { get; set; }
        public string GoogleMapApiKey { get; set; }
        public string TalkToKey { get; set; }

        //SMTP
        public string SmtpServer { get; set; }
        public int SmtpPort { get; set; }
        public bool EnableSsl { get; set; }
        public string SmtpFullName { get; set; }
        public string SmtpUsername { get; set; }
        public string SmtpPassword { get; set; }

        //offline, comming soon
        public bool IsOffline { get; set; }
        public bool IsCommingSoon { get; set; }
        public DateTime? OpeningDate { get; set; }
    }
}
