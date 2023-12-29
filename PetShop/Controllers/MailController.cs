using WebApiTutorialHE.Models;
using WebApiTutorialHE.Service;
using Microsoft.AspNetCore.Mvc;
using WebApiTutorialHE.Models.Mail;
using WebApiTutorialHE.Service.Interface;
using PetShop.Models.Contact;
using WebApiTutorialHE.Models.UtilsProject;
using Microsoft.Extensions.Options;

namespace MailKitDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MailController : ControllerBase
    {
        private readonly IMailService _mail;
        private readonly MailSettings _mailSettings;
        public MailController(IMailService mail , IOptions<MailSettings> mailSettings)
        {
            _mail = mail;
            _mailSettings = mailSettings.Value;
        }

        //[HttpPost("sendmail")]
        //public async Task<IActionResult> SendMailAsync([FromForm]MailDataWithAttachments mailData)
        //{
        //    bool result = await _mail.SendMail(mailData, new CancellationToken());

        //    if (result)
        //    {
        //        return StatusCode(StatusCodes.Status200OK, "Mail has successfully been sent.");
        //    }
        //    else
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, "An error occured. The Mail could not be sent.");
        //    }
        //}

        [HttpPost("sendmail")]
        public async Task<IActionResult> SendMailAsync([FromForm] ContactForm contactForm)
        {
            if (contactForm?.Email == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Email cannot be null.");
            }

            var email = _mailSettings.UserName; // Use _mailSettings instead of a new MailSettings object
            var mailData = new MailDataWithAttachments()
            {
                From = contactForm.Email, // this should be the user's email
                To = new List<string>()
        {
            email // this should be your email
        },
                Subject = contactForm.Subject,
                Body = $"Message from: {contactForm.Name}\n\n{contactForm.Message}"
            };

            bool result = await _mail.SendMail(mailData, new CancellationToken());

            if (result)
            {
                return StatusCode(StatusCodes.Status200OK, "Mail has successfully been sent.");
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occured. The Mail could not be sent.");
            }
        }


    }
}