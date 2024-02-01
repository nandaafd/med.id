using BATCH336A.DataAccess;
using BATCH336A.DataModel;
using BATCH336A.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace BATCH336A.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoleController : Controller
    {
        public DARole role;
        public RoleController(BATCH336AContext _db)
        {
            role = new DARole(_db);
        }

        [HttpGet]
        public VMResponse GetAll()
        {
            return role.GetAll();
        }

        [HttpGet("[action]/{id?}")]
        public VMResponse GetById(int id)
        {
            return role.GetById(id);
        }

    }
}
