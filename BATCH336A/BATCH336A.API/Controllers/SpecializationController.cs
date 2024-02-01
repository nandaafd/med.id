using BATCH336A.DataAccess;
using BATCH336A.DataModel;
using BATCH336A.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace BATCH336A.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SpecializationController : Controller
    {
        private DASpecialization specialization;
        public VMResponse response = new VMResponse();

        public SpecializationController(BATCH336AContext _db)
        {
            specialization = new DASpecialization(_db);
        }

        [HttpGet]
        public VMResponse GetAll() => specialization.GetByFilter();

        [HttpGet("[action]/{id?}")]
        public VMResponse GetById(long id) => specialization.GetById(id);

        [HttpGet("[action]/{name?}")]
        public VMResponse GetByName(string name) => specialization.GetByFilter(name);

        [HttpPost]
        public VMResponse Create(VMMSpecialization data) => specialization.CreateSpecialization(data);

        [HttpPut]
        public VMResponse Update(VMMSpecialization data) => specialization.UpdateSpecialization(data);

        [HttpDelete]
        public VMResponse Delete(long id, long userId) => specialization.DeleteSpecialization(id, userId);
    }
}
