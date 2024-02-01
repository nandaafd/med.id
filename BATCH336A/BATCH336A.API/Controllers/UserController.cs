using BATCH336A.DataAccess;
using BATCH336A.DataModel;
using BATCH336A.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace BATCH336A.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {

        public DAUser user;
        public UserController(BATCH336AContext _db)
        {
            user = new DAUser(_db);
        }

        [HttpGet]
        public VMResponse GetAll()
        {
            return user.GetAll();
        }

        [HttpGet("[action]/{id?}")]
        public VMResponse GetById(int id)
        {
            return user.GetById(id);
        }

        [HttpGet("[action]/{email?}")]
        public VMResponse GetByEmail(string email)
        {
            return user.GetByEmail(email);
        }
        [HttpPut]
        public VMResponse Update(VMMUser data)
        {
            return user.Update(data);
        }
    }
}
