using BATCH336A.Models;
using BATCH336A.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace BATCH336A.Controllers
{
    public class DetailDokterController : Controller
    {
        private readonly DetailDokterModel detaildokter;
        private readonly MenuModel? menuModel;
        private readonly RoleModel role;
        private VMResponse response = new VMResponse();

        public DetailDokterController(IConfiguration _config, IWebHostEnvironment _webhost) {
            detaildokter = new DetailDokterModel(_config);
            menuModel = new MenuModel(_config, _webhost);
        }

        public IActionResult Index() {
            //ViewBag.Role = role.GetAll();
            ViewBag.Menu = menuModel.GetAll();
            return View();
        }

        public IActionResult Details(long _id)
        {
            ViewBag.Profile = detaildokter.GetProfile(_id);
            ViewBag.Education = detaildokter.GetEducation(_id);
            ViewBag.Medis = detaildokter.GetMedis(_id);
            ViewBag.Riwayat = detaildokter.GetRiwayat(_id);
            ViewBag.Jadwal = detaildokter.GetJadwal(_id);
            ViewBag.Lokasi = detaildokter.GetLokasi(_id);
            ViewBag.Role = role.GetAll();
            ViewBag.Menu = menuModel.GetAll();
            return View();
        }
    }
}
