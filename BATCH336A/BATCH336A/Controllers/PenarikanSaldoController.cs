using BATCH336A.Models;
using BATCH336A.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace BATCH336A.Controllers
{
    public class PenarikanSaldoController : Controller
    {
        private readonly PenarikanSaldoModel pesa;
        private readonly MenuModel? menuModel;
        private readonly RoleModel role;
        private VMResponse response = new VMResponse();

        public PenarikanSaldoController(IConfiguration _config, IWebHostEnvironment _webhost)
        {
            pesa = new PenarikanSaldoModel(_config);
            menuModel = new MenuModel(_config, _webhost);
            role = new RoleModel(_config);
        }
        public IActionResult Index()
        {
            ViewBag.Role = role.GetAll();
            ViewBag.Menu = menuModel.GetAll();
            return View();
        }
        [HttpPost]
        public async Task<VMResponse> CekPin(VMPenarikanSaldo data,  string pin1, string pin2, string pin3, string pin4, string pin5)
        {
            data.Pin = pin1 + pin2 + pin3 + pin4 + pin5;
            response = await pesa.CekPin(data);
            return response;
        }
        public IActionResult Auth()
        {

            ViewBag.Title = "Authentikasi";
            return View();
        }
        public IActionResult Otp(int otp)
        {
            ViewBag.Otp = otp;
            ViewBag.Title = "Tarik Saldo";
            return View();
        }
        public IActionResult InputPin(decimal saldo, decimal transaksi, long id)
        {
            ViewBag.Saldo = saldo;
            ViewBag.Id = id;
            ViewBag.Transaksi = transaksi;
            ViewBag.Title = "Input PIN";
            ViewBag.Role = role.GetAll();
            ViewBag.Menu = menuModel.GetAll();
            return View();
        }
        [HttpPost]
        public async Task<VMResponse> Edit(VMPenarikanSaldo data)
        {
            if (!ModelState.IsValid)
            {
                response.statusCode = System.Net.HttpStatusCode.BadRequest;
                response.data = null;
                response.message = "Request tidak valid";
                return response;
            }
            response = await pesa.UpdateAsync(data);
            return response;
        }
        public IActionResult NominalLain(decimal saldo)
        {
            ViewBag.Saldo = saldo;
            ViewBag.Title = "Isi Nominal Lain";
            return View();
        }
        public IActionResult HalAwal(long id)
        {
            ViewBag.custId = id;
            return View();
        }
        public IActionResult TarikSaldo(long id)
        {
            VMPenarikanSaldo? custById = pesa.GetById(id);
            ViewBag.DefNom = pesa.GetDefNom();
            ViewBag.CustNom = pesa.GetCustNom(id);
            ViewBag.CustById = pesa.GetById(id);
            return View(custById);
        }
    }
}
