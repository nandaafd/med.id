using BATCH336A.DataModel;
using BATCH336A.ViewModel;
using Microsoft.AspNetCore.Mvc;
using BATCH336A.DataAccess;

namespace BATCH336A.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DetailDokterController : Controller
    {
        private DADetailDokter detaildokter;
        public DetailDokterController(BATCH336AContext _db)
        {
            detaildokter = new DADetailDokter(_db);
        }
        [HttpGet("[action]/{id?}")]
        public VMResponse Education(int id) => detaildokter.GetEducationById(id);
        [HttpGet("[action]/{id?}")]
        public VMResponse Profile(int id) => detaildokter.GetProfileById(id);
        [HttpGet("[action]/{id?}")]
        public VMResponse Medis(int id) => detaildokter.GetMedisById(id);
        [HttpGet("[action]/{id?}")]
        public VMResponse Riwayat(int id) => detaildokter.GetRiwayatById(id);
        [HttpGet("[action]/{id?}")]
        public VMResponse Jadwal(int id) => detaildokter.GetJadwalByMfId(id);
        [HttpGet("[action]/{id?}")]
        public VMResponse Lokasi(int id) => detaildokter.GetLokasiByDoctorId(id);
    }
}
