using BATCH336A.DataAccess;
using BATCH336A.DataModel;
using BATCH336A.Models;
using BATCH336A.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net;

namespace BATCH336A.Controllers
{

    public class PendaftaranController : Controller
    {
        private VMResponse response = new VMResponse();
        private PendaftaranModel pendaftaran;
        private TokenModel token;
        private readonly ILogger<HomeController> _logger;

        public PendaftaranController(ILogger<HomeController> logger, IConfiguration _config)
        {
            _logger = logger;
            pendaftaran = new PendaftaranModel(_config);
            token = new TokenModel(_config);
        }

        [HttpPost]
        public async Task<VMResponse> Add(VMMPendaftaran data)
        {
            response = await pendaftaran.CreateAsync(data);
            if (response.statusCode == HttpStatusCode.OK || response.statusCode == HttpStatusCode.Created)
            {
                HttpContext.Session.SetString("infoMsg", response.message);
            }
            else if (response.statusCode == HttpStatusCode.NotFound || response.statusCode == HttpStatusCode.NoContent || response.statusCode == HttpStatusCode.InternalServerError) 
            {
                HttpContext.Session.SetString("errMsg", response.message);
            }
            return response;
        }
        [HttpPost]
        public async Task<VMResponse> CheckEmail(string data)
        {
            response = await pendaftaran.GetByEmail(data);
            return response;
        }
        [HttpPost]
        public async Task<VMResponse> SendEmail(string email)
        {
            var random = new Random();
            int otp = random.Next(100000, 999999);

            List<VMTToken>? id = token.GetByEmail(email);
            if (id != null)
            {
                VMTToken data = id.FirstOrDefault();
                token.Update(data);
            }
            VMTToken tokenData = new VMTToken()
            {
                Email = email,
                UserId = 0,
                Token = otp.ToString(),
                UsedFor = "register",
                IsExpired = false,
                ExpiredOn = DateTime.Now.AddMinutes(3),
                CreatedBy = 0
            };
            token.CreateAsync(tokenData);

            response = await pendaftaran.SendEmail(email, otp);
            return response;
        }

        [HttpPost]
        public VMResponse VerifOtp(int InputOtp, string email)
        {
            int? expiredOtp = 0;
            VMTToken? kodeOtp = null;
            List<VMTToken>? getToken = token.GetByEmail(email);
            if (getToken != null)
            {
                kodeOtp = getToken.FirstOrDefault();
            }
            if(kodeOtp != null)
            {
                if (DateTime.Now > kodeOtp.ExpiredOn)
                {
                    expiredOtp = Int32.Parse(kodeOtp.Token);
                    token.Update(kodeOtp);
                    kodeOtp = null;
                }
                if (kodeOtp != null)
                {
                    if (InputOtp == Int32.Parse(kodeOtp.Token) && kodeOtp.IsExpired == false)
                    {
                        List<VMTToken>? getLatestToken = token.GetByEmail(email);
                        if (getLatestToken != null) {
                            VMTToken? latestToken = getLatestToken.FirstOrDefault();
                            token.Update(latestToken);
                        };
                        response.statusCode = HttpStatusCode.OK;
                    }
                    else if (getToken.Any(t => int.Parse(t.Token) == InputOtp && getToken.Any(t => t.IsExpired == true)))
                    {
                        response.statusCode = HttpStatusCode.NoContent;
                        response.message = "Kode OTP anda telah kadaluarsa, silahkan kirim ulang kode OTP";
                    }
                    else
                    {
                        response.statusCode = HttpStatusCode.NotFound;
                        response.message = "Kode OTP anda salah";
                    }
                }
                else
                {
                    if (InputOtp == expiredOtp || getToken.Any(t => int.Parse(t.Token) == InputOtp && getToken.Any(t => t.IsExpired == true)))
                    {
                        response.statusCode = HttpStatusCode.NoContent;
                        response.message = "Kode OTP anda telah kadaluarsa, silahkan kirim ulang kode OTP";
                    }
                    else
                    {
                        response.statusCode = HttpStatusCode.NotFound;
                        response.message = "Kode OTP anda salah";
                    }
                }
            }
            else
            {
                response.statusCode = HttpStatusCode.InternalServerError;
                response.message = "server bermasalah";
            }

            return response;
        }

    }
}

