using Microsoft.AspNetCore.Mvc;
using BATCH336A.DataAccess;
using BATCH336A.DataModel;
using BATCH336A.API.Controllers;
using BATCH336A.ViewModel;
using System.Diagnostics.Metrics;

namespace BATCH336A.API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class ProfileController : Controller
    {
        private DAProfile profile;
        public VMResponse response = new VMResponse();
        public ProfileController(BATCH336AContext _profile)
        {
            profile = new DAProfile(_profile);
        }

        [HttpGet]
        public VMResponse GetAll() => profile.GetAll();
        [HttpGet("[action]/{id?}")]
        public VMResponse Get(int id) => profile.GetById(id);

        [HttpGet("[action]/{email?}")]
        public VMResponse GetByEmail(string email)
        {
            return profile.GetByEmail(email);
        }
        [HttpPut]
        public VMResponse Update(VMProfile data)
        {
            return profile.Update(data);
        }
    }
}
