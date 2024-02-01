using BATCH336A.DataAccess;
using BATCH336A.DataModel;
using BATCH336A.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace BATCH336A.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CariDokterController : Controller
    {
        private DACariDokter cariDokter;
        public VMResponse response = new VMResponse();

        public CariDokterController(BATCH336AContext _db)
        {
            cariDokter = new DACariDokter(_db);
        }
        [HttpGet]
        public VMResponse GetAll() => cariDokter.GetAll();

        //[HttpGet("[action]/{location?}/{spec?}/{name?}/{treat?}")]
        [HttpGet("[action]")]
        public VMResponse Get(string? location, string spec, string? name, string? treat) => cariDokter.Get(location, spec, name, treat);

        [HttpGet("[action]")]
        public VMResponse GetLoc() => cariDokter.Getloc();

        [HttpGet("[action]")]
        public VMResponse GetTreatment() => cariDokter.GetTreatment();

    }
}
