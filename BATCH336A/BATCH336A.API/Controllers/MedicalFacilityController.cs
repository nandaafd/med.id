using BATCH336A.DataAccess;
using BATCH336A.DataModel;
using BATCH336A.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace BATCH336A.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MedicalFacilityController : Controller
    {
        private DAMedicalFacilityCategory medicalFacilityCategory;
        public MedicalFacilityController(BATCH336AContext _db)
        {
            medicalFacilityCategory = new DAMedicalFacilityCategory(_db);
        }

        [HttpGet("[action]/{filter}")]
        public VMResponse Search(string? filter) => medicalFacilityCategory.GetAllByFilter(filter);

        [HttpGet]
        public VMResponse GetAll() => medicalFacilityCategory.GetAll();

        [HttpGet("[action]/{id?}")]
        public VMResponse GetById(int id) => medicalFacilityCategory.GetById(id);

        [HttpPost]
        public VMResponse Create(VMMMedicalFacilityCategory data) => medicalFacilityCategory.CreateData(data);

        [HttpPut("[action]")]
        public VMResponse Update(VMMMedicalFacilityCategory data) => medicalFacilityCategory.Update(data);

        [HttpDelete("[action]/{id?}/{userId?}")]
        public VMResponse Delete(long id,long userId) => medicalFacilityCategory.Delete(id,userId);
    }
        
}
