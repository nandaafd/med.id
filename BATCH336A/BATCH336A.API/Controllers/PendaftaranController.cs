using BATCH336A.DataAccess;
using BATCH336A.ViewModel;
using Microsoft.AspNetCore.Mvc;
using MimeKit.Text;
using MimeKit;
using BATCH336A.DataModel;
using MailKit.Net.Smtp;

namespace BATCH336A.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PendaftaranController : Controller
    {
        private DAPendaftaran pendaftaran;
        public VMResponse response = new VMResponse();

        public PendaftaranController(BATCH336AContext _db)
        {
            pendaftaran = new DAPendaftaran(_db);
        }

        [HttpPost]
        public VMResponse Pendaftaran(VMMPendaftaran data)
        {
            return pendaftaran.CreatePendaftaran(data);
        }

        [HttpGet("[action]/{email?}")]
        public VMResponse GetByEmail(string email)
        {
            return pendaftaran.GetByEmail(email);
        }

        [HttpPost("[action]/{target?}/{otp?}")]
        public VMResponse SendEmail(string target, int otp)
        {
            try
            {
                var email = new MimeMessage();
                email.From.Add(MailboxAddress.Parse("siprakerin.smkiu@gmail.com"));
                email.To.Add(MailboxAddress.Parse(target));
                email.Subject = "Med.Id Verifikasi OTP";
                email.Body = new TextPart(TextFormat.Html)
                {
                    Text = "" +
                    "<h3>Med.Id Verifikasi OTP</h3><br/>" +
                    "<p>Kode OTP akan kadaluarsa dalam 10 Menit</p><br>" +
                    "<h5>Kode OTP</h5></br>" +
                    $"<div><h3><b>{otp}</b></h3></div>"
                };
                using var smtp = new SmtpClient();
                smtp.Connect("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
                smtp.Authenticate("siprakerin.smkiu@gmail.com", "vmli jstl pxwf osuc"); //vmli jstl pxwf osuc
                smtp.Send(email);
                smtp.Disconnect(true);

                response.statusCode = System.Net.HttpStatusCode.OK;
                response.message = "Berhasil mengirim email kode OTP";
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
                response.statusCode = System.Net.HttpStatusCode.InternalServerError;
            }

            return response;
        }
    }
}
