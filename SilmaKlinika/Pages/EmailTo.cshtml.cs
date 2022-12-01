using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SilmaKlinika.Pages.Models;
using System.Net.Mail;

namespace SilmaKlinika.Pages
{
    public class EmailToModel : PageModel
    {
        public void OnGet()
        {
        }
        [BindProperty]
        public Email sendmail { get; set; }
        public async Task OnPost()
        {
            string To = sendmail.To;
            string Subject = "Kohtumine";
            string Body = "Olete edukalt broneerinud aja arsti juurde"; //+ TooNimi
            MailMessage mm = new MailMessage();
            mm.To.Add(To);
            mm.Subject = Subject;
            mm.Body = Body;
            mm.IsBodyHtml = false;
            mm.From = new MailAddress("silmaklinik@gmail.com");
            SmtpClient smtp = new SmtpClient("smtp.gmail.com");
            smtp.Port = 587;
            smtp.UseDefaultCredentials = true;
            smtp.EnableSsl = true;
            smtp.Credentials = new System.Net.NetworkCredential("silmaklinik@gmail.com", "Traktorist300");
            await smtp.SendMailAsync(mm);
            ViewData["Message"] = "Post on saadetud aadressile " + sendmail.To;
        }
    }
}