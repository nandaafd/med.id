using BATCH336A.DataAccess;
using BATCH336A.DataModel;
using BATCH336A.Models;
using BATCH336A.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Drawing.Printing;

namespace BATCH336A.Controllers
{
    public class BuatJanjiController : Controller
    {
        private VMResponse response = new VMResponse();
        private readonly MenuModel? menuModel;
        private readonly RoleModel role;
        private BuatJanjiModel buatJanji;

        public BuatJanjiController(IConfiguration _config, IWebHostEnvironment environment)
        {
            menuModel = new MenuModel(_config, environment);
            buatJanji = new BuatJanjiModel(_config);
        }
        public IActionResult Index(int id)
        {
            ViewBag.Role = role.GetAll();
            ViewBag.Menu = menuModel.GetAll();
            VMCariDokter data = buatJanji.GetDocById(id);
            ViewBag.Customer = buatJanji.GetCustById((int)HttpContext.Session.GetInt32("userBiodataId"));
            return View(data);
        }

        public IActionResult Add(int id, int? faskesId)
        {
            VMCariDokter data = buatJanji.GetDocById(id);
            ViewBag.FaskesId = faskesId;
            
            if(HttpContext.Session.GetInt32("userBiodataId") != null)
            {
                ViewBag.Customer = buatJanji.GetCustById((int)HttpContext.Session.GetInt32("userBiodataId"));

            }
            ViewBag.Title = "Buat Janji Dokter";
            ViewBag.Role = role.GetAll();
            ViewBag.Menu = menuModel.GetAll();
            return View(data);
        }

        [HttpPost]
        public async Task<VMResponse> Add(VMTAppointment data)
        {
            int ? slot = buatJanji.GetSlot((long)data.DoctorOfficeScheduleId).Slot;
            string date = data.AppointmentDate?.ToString("yyyy-MM-dd")!;
            int janji = (buatJanji.GetApp((long)data.DoctorOfficeScheduleId, date)) == null ? 0 : (buatJanji.GetApp((long)data.DoctorOfficeScheduleId, date)).Count();

            List<VMTAppointment> a = buatJanji.GetApp((long)data.DoctorOfficeScheduleId, date);
            if(a != null)
            {
                VMTAppointment? b = a.Where(cust => cust.CustomerId == data.CustomerId).FirstOrDefault();
                if (b != null)
                {
                    response.message = "Janji sudah dibuat";
                    response.statusCode = System.Net.HttpStatusCode.Ambiguous;
                    return response;
                }
            }

            if (slot > janji)
            {
                response = await buatJanji.CreateAsync(data);
            }
            else
            {
                response.message = "Jadwal Penuh";
                response.statusCode = System.Net.HttpStatusCode.BadRequest;
            }

            return response;
        }

        public List<VMMMedicalFacilitySchedule>? GetJadwal(long offId, DateTime appDate)
        {
            string appDate2 = appDate.ToString("yyyy-MM-dd");

            List<VMMMedicalFacilitySchedule>? jadwal = new List<VMMMedicalFacilitySchedule>();

            if (appDate.Date != DateTime.Now.Date)
            {
                jadwal =
                buatJanji.GetJadwal(offId).Where(
                    a => a.slot > (
                    (buatJanji.GetApp((long)a.doctorOfficeScheduleId, appDate2)) == null ? 0 : (buatJanji.GetApp((long)a.doctorOfficeScheduleId, appDate2)).Count())).ToList();
            }
            else
            {
                jadwal =
                buatJanji.GetJadwal(offId).Where(
                    a => a.slot > (
                    (buatJanji.GetApp((long)a.doctorOfficeScheduleId, appDate2)) == null ? 0 : (buatJanji.GetApp((long)a.doctorOfficeScheduleId, appDate2)).Count())
                    && DateTime.Parse(a.TimeScheduleEnd) > DateTime.Now).ToList();
            }
            

            return jadwal;
        }

        public List<VMMTindakan>? GetTreatment(long offId)
        {
            return buatJanji.GetTreatment(offId);
        }
    }
}
