using BATCH336A.AddOns;
using BATCH336A.Models;
using BATCH336A.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Drawing.Printing;

namespace BATCH336A.Controllers
{
    public class CariDokterController : Controller
    {
        private VMResponse response = new VMResponse();
        private CariDokterModel cariDokter;
        private SpecializationModel specialization;
        private readonly int pageSize;
        private readonly MenuModel? menuModel;
        private readonly RoleModel role;

        public CariDokterController(IConfiguration _config, IWebHostEnvironment environment)
        {
            cariDokter = new CariDokterModel(_config);
            specialization = new SpecializationModel(_config);
            pageSize = int.Parse(_config["PageSize"]);
            menuModel = new MenuModel(_config, environment);
        }
        public IActionResult Index(string location, string spec, string name, string treat, int? pageNumber, int? currPageSize)
        {
            List<VMCariDokter>? data;
            if (location == null && spec==null && name == null && treat == null)
            {
                data = cariDokter.GetAll();
            }
            else
            {
                data = cariDokter.GetBy(location, spec, name, treat);
            }

            if (data == null)
            {
                data = new List<VMCariDokter>();
            }

            ViewBag.PageSize = (currPageSize ?? pageSize);
            ViewBag.Location = location;
            ViewBag.Specialization = spec;
            ViewBag.Name = name;
            ViewBag.Treatment = treat;
            ViewBag.Role = role.GetAll();
            ViewBag.Menu = menuModel.GetAll();
            ViewBag.Title = "Cari Dokter";

            return View(Pagination<VMCariDokter>.Create(data, pageNumber ?? 1, ViewBag.PageSize));
        }

        public IActionResult Search()
        {
            ViewBag.Locations = cariDokter.GetLoc();
            ViewBag.Specializations = specialization.GetAll();
            ViewBag.Treatments = cariDokter.GetTreatment();
            ViewBag.Title = "Cari Dokter";
            ViewBag.Role = role.GetAll();
            ViewBag.Menu = menuModel.GetAll();
            return View();
        }
    }
}
