using BATCH336A.DataAccess;
using BATCH336A.DataModel;
using BATCH336A.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace BATCH336A.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BuatJanjiController : Controller
    {
        private DABuatJanji buatJanji;
        public VMResponse response = new VMResponse();

        public BuatJanjiController(BATCH336AContext _db)
        {
            buatJanji = new DABuatJanji(_db);
        }

        [HttpGet("[action]")]
        public VMResponse GetCust(int id) => buatJanji.GetCust(id);

        [HttpGet("[action]")]
        public VMResponse GetDoc(int id) => buatJanji.GetDoc(id);

        [HttpGet("[action]")]
        public VMResponse GetJadwal(long offId) => buatJanji.GetJadwal(offId);

        [HttpGet("[action]")]
        public VMResponse GetTreatment(long offId) => buatJanji.GetTreatment(offId);
    }
}
