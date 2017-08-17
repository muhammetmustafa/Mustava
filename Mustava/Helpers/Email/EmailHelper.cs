using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using Mustava.Extensions;

namespace Mustava.Helpers.Email
{
    public class EmailHelper
    {
        private static SmtpInfoDto smtp;

        static EmailHelper()
        {
            Initialize();
        }

        private static void Initialize()
        {
            //smtp =
            //    new SqlHelper().Query(
            //        new SqlCommand(
            //                "SELECT * From SmtpMailAccounts WHERE Id = 2 AND IsActive = 1 AND IsDeleted = 0")).ParseFirst<SmtpInfoDto>();
            smtp = new SmtpInfoDto()
            {
                SmtpServer = "mail.ldcdanismanlik.com",
                Frm = "kamelon@ldcdanismanlik.com",
                Port = 587,
                Username = "kamelon@ldcdanismanlik.com",
                Password = "KaM3L0n2014"
            };
        }

        public bool Send(string to, string attachmentName, Stream attachmentSource)
        {
            if (smtp == null)
            {
                Initialize();
                if (smtp == null)
                {
                    return false;
                }
            }

            var emailData = new EmailModel()
            {
                To = to,
                Message = "Roster Report",
                Subject = "Roster Report"
            };

            return Send(smtp, emailData, attachmentName, attachmentSource);
        }

        public static bool Send(EmailModel email, string attachmentName, Stream attachmentSource)
        {
            return Send(smtp, email, attachmentName, attachmentSource);
        }

        public static bool Send(SmtpInfoDto smtpInfoDto, EmailModel email, string attachmentName, Stream attachmentSource)
        {
            if (smtpInfoDto == null || email == null)
            {
                return false;
            }

            var mail = new MailMessage(smtpInfoDto.Frm, email.To);

            if (email.CarbonCopies != null)
            {
                email.CarbonCopies.ForEach(carbonCopy => mail.CC.Add(carbonCopy));
            }

            var client = new SmtpClient()
            {
                Port = smtpInfoDto.Port,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Host = smtpInfoDto.SmtpServer,
                Credentials = new NetworkCredential(smtpInfoDto.Username, smtpInfoDto.Password),
                Timeout = 10 * 60 * 1000
            };

            if (!attachmentName.ExIsNullOrEmpty() && attachmentSource != null)
            {
                attachmentSource.Position = 0;
                var attachment = new Attachment(attachmentSource, MediaTypeNames.Application.Pdf);
                attachment.ContentDisposition.FileName = attachmentName + ".pdf";
                mail.Attachments.Add(attachment);
            }

            mail.Subject = email.Subject;
            mail.Body = email.Message;
            mail.IsBodyHtml = true;

            client.Send(mail);

            return true;
        }
    }
}