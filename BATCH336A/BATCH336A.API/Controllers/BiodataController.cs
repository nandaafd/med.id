using BATCH336A.DataAccess;
using BATCH336A.DataModel;
using BATCH336A.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace BATCH336A.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BiodataController : Controller
    {
        private DABiodata? biodata;
        public BiodataController(BATCH336AContext db)
        {
            biodata = new DABiodata(db);
        }
        [HttpGet]
        public VMResponse GetAll() => biodata.GetAll();
        [HttpGet("[action]/{id?}")]
        public VMResponse GetById(long id) => biodata.GetById(id);

    }
}
