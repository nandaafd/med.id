using BATCH336A.Models;
using BATCH336A.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net;

namespace BATCH336A.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private VMResponse response = new VMResponse();
        private PendaftaranController pendaftaranController;
        private readonly MenuModel? menuModel;
        private readonly RoleModel role;
        public HomeController(ILogger<HomeController> logger, IWebHostEnvironment _webHostEnv, IConfiguration _config)
        {
            menuModel = new MenuModel(_config, _webHostEnv);
            _logger = logger;
            role = new RoleModel(_config);
        }

        public IActionResult Index()
        {
            var errMsg = HttpContext.Session.GetString("errMsg");
            var infoMsg = HttpContext.Session.GetString("infoMsg");

            if (errMsg != "")
            {
                ViewBag.ErrMsg = errMsg;
                HttpContext.Session.Remove("errMsg"); // Bersihkan session
            }
            else if (infoMsg != "")
            {
                ViewBag.InfoMsg = infoMsg;
                HttpContext.Session.Remove("infoMsg"); // Bersihkan session
            }
            ViewBag.Role = role.GetAll();
            ViewBag.Menu = menuModel.GetAll();
            List<VMMMenu>? data = menuModel.GetAll();
            return View(data);
        }
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Add(VMMMenu data)
        {
            menuModel.Create(data);
            return RedirectToAction("Index", "Home");
        }
        public IActionResult Edit(int id)
        {
            VMMMenu? data = menuModel.GetById(id);
            return View(data);
        }

        [HttpPost]
        public IActionResult? Edit(VMMMenu data)
        {
            menuModel.Update(data);
            return RedirectToAction("Index","Home");
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
