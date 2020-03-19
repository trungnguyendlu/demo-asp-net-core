using Demo.Data;
using System;
using System.ComponentModel.DataAnnotations;

namespace Demo.Model.Admin.Settings
{
    public class SettingsModel : BaseModel
    {
        [Display(Name = "Tên website")]
        public string SiteName { get; set; }
        [Display(Name = "Loại tiêu đề website")]
        public TitleTemplateType TitleTemplateType { get; set; }
        [Display(Name = "Phân cách tiêu đề website")]
        public string TitleSeparate { get; set; }
        [Display(Name = "Phiên bản")]
        public string Version { get; set; }
        [Display(Name = "Từ khóa")]
        public string Keywords { get; set; }
        [Display(Name = "Mô tả website")]
        public string Description { get; set; }
        [Display(Name = "Đường dẫn Logo")]
        public string IconUrl { get; set; }
        [Display(Name = "Số bài viết hiển thị trên 1 trang")]
        public PageSizeType PageSize { get; set; }


        [Display(Name = "Tên Công ty")]
        public string CompanyName { get; set; }
        [Display(Name = "Địa chỉ")]
        public string Address { get; set; }
        [Display(Name = "Email")]
        public string EmailContact { get; set; }
        [Display(Name = "Điện thoại")]
        public string PhoneNumber { get; set; }

        //Social
        [Display(Name = "Facebook")]
        public string FacebookUrl { get; set; }
        [Display(Name = "Twitter")]
        public string TwitterUrl { get; set; }
        [Display(Name = "Google Plus")]
        public string GooglePlusUrl { get; set; }
        [Display(Name = "Youtube")]
        public string YoutubeUrl { get; set; }
        [Display(Name = "Linked In")]
        public string LinkedInUrl { get; set; }
        [Display(Name = "Pinterest")]
        public string PinterestUrl { get; set; }
        [Display(Name = "Instagram")]
        public string InstagramUrl { get; set; }


        [Display(Name = "Google Console Verification")]
        public string GoogleConsoleVerification { get; set; }
        [Display(Name = "Yandex Verification")]
        public string YandexVerification { get; set; }
        [Display(Name = "Bing Verification")]
        public string BingVerification { get; set; }


        [Display(Name = "Key Google Analytics")]
        public string GoogleAnalyticsKey { get; set; }
        [Display(Name = "Facebook Pixel Id")]
        public string FacebookPixelId { get; set; }
        [Display(Name = "reCAPTCHA Site Key")]
        public string ReCaptchaSiteKey { get; set; }
        [Display(Name = "reCAPTCHA Secret Key")]
        public string ReCaptchaSecretKey { get; set; }
        [Display(Name = "Talk.to Id")]
        public string TalkToKey { get; set; }


        [Display(Name = "Server")]
        public string SmtpServer { get; set; }
        [Display(Name = "Port")]
        public int SmtpPort { get; set; }
        [Display(Name = "Cho phép SSL")]
        public bool SmtpEnableSsl { get; set; }
        [Display(Name = "Tên hiển thị")]
        public string SmtpFullName { get; set; }
        [Display(Name = "Username")]
        public string SmtpUsername { get; set; }
        [Display(Name = "Password")]
        public string SmtpPassword { get; set; }


        [Display(Name = "Bảo trì hệ thống")]
        public bool IsOffline { get; set; }
        [Display(Name = "Sắp khai trương")]
        public bool IsCommingSoon { get; set; }
        [Display(Name = "Ngày khai trương")]
        public DateTime? OpeningDate { get; set; }

    }
}
