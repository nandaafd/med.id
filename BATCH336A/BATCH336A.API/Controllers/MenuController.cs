using BATCH336A.DataAccess;
using BATCH336A.DataModel;
using BATCH336A.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace BATCH336B.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MenuController : Controller
    {
        public DAMenu menu;

        public MenuController(BATCH336AContext _db)
        {
            menu = new DAMenu(_db);
        }

        [HttpGet]
        public VMResponse GetAll()
        {
            return menu.GetAll();
        }
        [HttpGet("[action]/{id?}")]
        public VMResponse GetById(long id)
        {
            return menu.GetById(id);
        }

        [HttpPost]
        public VMResponse Create(VMMMenu data)
        {
            return menu.Create(data);
        }
        [HttpPut]
        public VMResponse Update(VMMMenu data)
        {
            return menu.Update(data);
        }

        [HttpDelete("[action]/{id?}/{userId?}")]
        public VMResponse Delete(int id, int userId)
        {
            return menu.Delete(id, userId);
        }
    }
}
