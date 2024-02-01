using BATCH336A.DataAccess;
using BATCH336A.DataModel;
using BATCH336A.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace BATCH336A.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EducationLevelController : Controller
    {
        private DAEducationLevel educationLevel;
        public EducationLevelController(BATCH336AContext _db)
        {
            educationLevel = new DAEducationLevel(_db);
        }

        [HttpGet("[action]/{filter}")]
        public VMResponse Search(string? filter) => educationLevel.GetAllByFilter(filter);

        [HttpGet]
        public VMResponse GetAll() => educationLevel.GetAll();

        [HttpGet("[action]/{id?}")]
        public VMResponse GetById(int id) => educationLevel.GetById(id);

        [HttpPost]
        public VMResponse Create(VMEducationLevel data) => educationLevel.CreateData(data);

        [HttpPut("[action]")]
        public VMResponse Update(VMEducationLevel data) => educationLevel.Update(data);

        [HttpDelete("[action]/{id?}/{userId?}")]
        public VMResponse Delete(long id, long userId) => educationLevel.Delete(id, userId);
    }
}
