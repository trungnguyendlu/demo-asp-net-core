using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Demo.Infrastructure.Helper.Email
{
    public static class EmailHelper
    {
        //public static Task SendEmailCheckoutSuccessAsync(CheckoutModel model, string orderUrl, string productUrl, EmailConfiguration config = null)
        //{
        //    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "emailtemplate", "payment.html");
        //    var htmlTemplate = File.ReadAllText(path);
        //    htmlTemplate = htmlTemplate.Replace("{{Name}}", model.Name)
        //        .Replace("{{Email}}", model.Email)
        //        .Replace("{{PhoneNumber}}", model.PhoneNumber)
        //        .Replace("{{OrderUrl}}", orderUrl)
        //        .Replace("{{OrderNumber}}", model.OrderNumber)
        //        .Replace("{{OrderDate}}", DateTime.Now.ToShortDateTimeString())
        //        .Replace("{{TotalAmount}}", model.TotalAmount.ToString("N0"))
        //        .Replace("{{ProductUrl}}", productUrl)
        //        .Replace("{{ProductName}}", model.Product.Name)
        //        .Replace("{{Package}}", model.Package.GetDescription())
        //        .Replace("{{Period}}", model.Period.GetDescription());

        //    return SendEmailAsync(
        //        config ?? Common.DefaultConfig,
        //        new EmailMessage
        //        {
        //            From = config?.SmtpUsername ?? Common.DefaultConfig.SmtpUsername,
        //            To = model.Email,
        //            Subject = $"ĐẶT HÀNG THÀNH CÔNG ĐƠN HÀNG {model.OrderNumber}",
        //            Content = htmlTemplate
        //        });
        //}

        //public static Task SendEmailCompleteOrderAsync(OrderModel model, EmailConfiguration config = null)
        //{
        //    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "emailtemplate", "order-complete.html");
        //    var htmlTemplate = File.ReadAllText(path);
        //    htmlTemplate = htmlTemplate.Replace("{{Name}}", model.Name)
        //        .Replace("{{OrderNumber}}", model.OrderNumber)
        //        .Replace("{{Domain}}", model.Domain)
        //        .Replace("{{AdminUrl}}", $"{model.Domain.TrimEnd('/')}/admin")
        //        .Replace("{{Username}}", model.Email)
        //        .Replace("{{Password}}", "asd@123")
        //        .Replace("{{ExpireDate}}", model.End.ToDateNullString());

        //    return SendEmailAsync(
        //        config ?? Common.DefaultConfig,
        //        new EmailMessage
        //        {
        //            From = config?.SmtpUsername ?? Common.DefaultConfig.SmtpUsername,
        //            To = model.Email,
        //            Subject = $"ĐƠN HÀNG {model.OrderNumber} ĐÃ HOÀN TẤT",
        //            Content = htmlTemplate
        //        });
        //}

        //public static Task SendEmailCompleteTrialAsync(TrialModel model, EmailConfiguration config = null)
        //{
        //    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "emailtemplate", "trial-complete.html");
        //    var htmlTemplate = File.ReadAllText(path);
        //    htmlTemplate = htmlTemplate.Replace("{{Name}}", model.Name)
        //        .Replace("{{Domain}}", model.Domain)
        //        .Replace("{{AdminUrl}}", $"{model.Domain.TrimEnd('/')}/admin")
        //        .Replace("{{Username}}", model.Email)
        //        .Replace("{{Password}}", "asd@123")
        //        .Replace("{{ExpireDate}}", model.End.ToDateNullString());

        //    return SendEmailAsync(
        //        config ?? Common.DefaultConfig,
        //        new EmailMessage
        //        {
        //            From = config?.SmtpUsername ?? Common.DefaultConfig.SmtpUsername,
        //            To = model.Email,
        //            Subject = $"{model.ProductName} ĐÃ CÀI ĐẶT HOÀN TẤT",
        //            Content = htmlTemplate
        //        });
        //}

        //public static Task SendEmailCancelOrderAsync(OrderModel model, string productUrl, EmailConfiguration config = null)
        //{
        //    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "emailtemplate", "order-cancel.html");
        //    var htmlTemplate = File.ReadAllText(path);
        //    htmlTemplate = htmlTemplate.Replace("{{Name}}", model.Name)
        //        .Replace("{{Email}}", model.Email)
        //        .Replace("{{PhoneNumber}}", model.PhoneNumber)
        //        .Replace("{{OrderNumber}}", model.OrderNumber)
        //        .Replace("{{TotalAmount}}", model.TotalAmount.ToString("N0"))
        //        .Replace("{{ProductUrl}}", productUrl)
        //        .Replace("{{ProductName}}", model.ProductName)
        //        .Replace("{{Package}}", model.Package.GetDescription())
        //        .Replace("{{Period}}", model.Period.GetDescription());

        //    return SendEmailAsync(
        //        config ?? Common.DefaultConfig,
        //        new EmailMessage
        //        {
        //            From = config?.SmtpUsername ?? Common.DefaultConfig.SmtpUsername,
        //            To = model.Email,
        //            Subject = $"ĐƠN HÀNG {model.OrderNumber} ĐÃ BỊ HỦY",
        //            Content = htmlTemplate
        //        });
        //}

        public static Task SendEmailAsync(EmailConfiguration emailConfiguration, EmailMessage emailMessage)
        {
            var client = new SmtpClient(emailConfiguration.SmtpServer)
            {
                EnableSsl = emailConfiguration.EnableSsl,
                UseDefaultCredentials = false,
                Port = emailConfiguration.SmtpPort,
                Credentials = new NetworkCredential(emailConfiguration.SmtpUsername, emailConfiguration.SmtpPassword)
            };
            var mailMessage = new MailMessage
            {
                IsBodyHtml = true,
                From = new MailAddress(emailMessage.From, emailConfiguration.SmtpFullName),
                Subject = emailMessage.Subject,
                Body = emailMessage.Content
            };
            mailMessage.To.Add(emailMessage.To);
            return client.SendMailAsync(mailMessage);
        }
    }
}
