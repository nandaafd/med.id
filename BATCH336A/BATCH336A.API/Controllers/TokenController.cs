using BATCH336A.DataAccess;
using BATCH336A.DataModel;
using BATCH336A.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace BATCH336A.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TokenController : Controller
    {
        private DAToken token;
        public VMResponse response = new VMResponse();
        public TokenController(BATCH336AContext _db)
        {
            token = new DAToken(_db);
        }
        [HttpGet]
        public VMResponse GetAll() => token.GetAll();
        [HttpGet("[action]/{id?}")]
        public VMResponse GetById(long id) => token.GetById(id);
        [HttpGet("[action]/{email?}")]
        public VMResponse GetByEmail(string email) => token.GetByEmail(email);
        [HttpPost]
        public VMResponse Create(VMTToken data) => token.Create(data);
        [HttpPut]
        public VMResponse Edit(VMTToken data) => token.Update(data);
    }
}
