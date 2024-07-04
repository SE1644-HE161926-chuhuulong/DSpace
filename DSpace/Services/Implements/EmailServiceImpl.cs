using MimeKit;
using MailKit.Net.Smtp;
using System.Globalization;
using Application.Emails;


namespace DSpace.Services.Implements
{
   public class EmailServiceImpl : EmailService
   {
      protected IConfiguration _configuration;
      private  string Email;
      private string PasswordApp;
      private string Host;
      private string Port;
      private string DisplayName;


      public EmailServiceImpl(IConfiguration configuration)
      {
         _configuration = configuration;
         Email = _configuration.GetValue<string>("EmailSettings:Email");
         PasswordApp = _configuration.GetValue<string>("EmailSettings:PasswordApp");
         Host = _configuration.GetValue<string>("EmailSettings:Host");
         Port = _configuration.GetValue<string>("EmailSettings:Port");
         DisplayName = _configuration.GetValue<string>("EmailSettings:DisplayName");


      }

      public void SendEmail(EmailDTO emailDTO)
      {
         var email = new MimeMessage();
         //Add mail sent
         //var emailSend;


        
         email.From.Add(new MailboxAddress(DisplayName, Email));
         //Add mail receive Address
         email.To.Add(new MailboxAddress("", emailDTO.email));

         email.Subject = emailDTO.subject;
         //email.Body = emailDTO.body;
         email.Body = new TextPart(MimeKit.Text.TextFormat.Html)
         {
            Text = emailDTO.body
         };

         using (var smtp = new SmtpClient())
         {
            smtp.Connect(Host, int.Parse(Port), false);

            // Note: only needed if the SMTP server requires authentication
            smtp.Authenticate(Email, PasswordApp);

            smtp.Send(email);
            smtp.Disconnect(true);
         }
      }
      public void SendEmailBcc(List<string> emailBcc, string subject, TextPart body)
      {
         var email = new MimeMessage();
         //Add mail sent
         //var emailSend;
         var EmailBcc = new List<MailboxAddress>();
         foreach (var e in emailBcc) {
            EmailBcc.Add(new MailboxAddress("", e));
         }

         
         
         email.From.Add(new MailboxAddress(DisplayName, Email));
         //Add mail receive Address
         //email.To.Add(new MailboxAddress("", emailTo));
         email.Bcc.AddRange(EmailBcc);

         email.Subject = subject;
         email.Body = body;
         //email.Body = new TextPart(MimeKit.Text.TextFormat.Html)
         //{
         //   Text = "<b>Hello all the way from the land of C#</b>"
         //};

         using (var smtp = new SmtpClient())
         {
            smtp.Connect(Host, int.Parse(Port), false);

            // Note: only needed if the SMTP server requires authentication
            smtp.Authenticate(Email, PasswordApp);

            smtp.Send(email);
            smtp.Disconnect(true);
         }
      }
   }
}
