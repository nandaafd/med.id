using BATCH336A.DataAccess;
using BATCH336A.DataModel;
using BATCH336A.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace BATCH336A.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MedicalItemCategoryController : Controller
    {
        private DAMedicalItemCategory medicalItemCategory;
        public VMResponse response = new VMResponse();

        public MedicalItemCategoryController(BATCH336AContext _db)
        {
            medicalItemCategory = new DAMedicalItemCategory(_db);
        }

        [HttpGet]
        public VMResponse GetAll() => medicalItemCategory.GetByFilter();

        [HttpGet("[action]/{id?}")]
        public VMResponse GetById(long id) => medicalItemCategory.GetById(id);

        [HttpGet("[action]/{name?}")]
        public VMResponse GetByName(string name) => medicalItemCategory.GetByFilter(name);

        [HttpPost]
        public VMResponse Create(VMMMedicalItemCategory data) => medicalItemCategory.CreateMedicalItemCategory(data);

        [HttpPut]
        public VMResponse Update(VMMMedicalItemCategory data) => medicalItemCategory.UpdateMedicalItemCategory(data);

        [HttpDelete]
        public VMResponse Delete(long id, long userId) => medicalItemCategory.DeleteMedicalItemCategory(id, userId);
    }
}
