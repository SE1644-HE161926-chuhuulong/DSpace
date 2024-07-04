using Application.Emails;
using DSpace.Services;
using Microsoft.AspNetCore.Mvc;

namespace DSpace.Controllers
{
   [Route("api/[controller]")]
   [ApiController]
   public class EmailController : Controller
   {
      EmailService _emailService;

      public EmailController(EmailService emailService)
      {
         _emailService = emailService;
      }

      [HttpPost("[action]")]
      public async Task<IActionResult> SendEmailTo(EmailDTO emailDTO) {
         _emailService.SendEmail(emailDTO);
         return Ok();

      }
   }
}
