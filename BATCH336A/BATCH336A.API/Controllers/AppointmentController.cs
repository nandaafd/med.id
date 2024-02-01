using BATCH336A.DataAccess;
using BATCH336A.DataModel;
using BATCH336A.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace BATCH336A.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AppointmentController : Controller
    {
        private DAAppointment appointment;
        public VMResponse response = new VMResponse();

        public AppointmentController(BATCH336AContext _db)
        {
            appointment = new DAAppointment(_db);
        }

        [HttpPost]
        public VMResponse CreateAppointment(VMTAppointment data) => appointment.CreateAppointment(data);

        [HttpGet("[action]")]
        public VMResponse GetApp(long schedId, string appDate) => appointment.Get(schedId, appDate);

        [HttpGet("[action]")]
        public VMResponse GetSlot(long id) => appointment.GetSlot(id);
    }
}
