using Application.Emails;
using MimeKit;

namespace DSpace.Services
{
   public interface EmailService
   {
      public void SendEmail(EmailDTO emailDTO);

      public void SendEmailBcc(List<string> emailBcc, string subject, TextPart body);
   }
}
