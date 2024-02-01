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

    public class CourierController : Controller
    {
        private DAMCourier courier;
        public VMResponse  response = new VMResponse();
        public CourierController(BATCH336AContext _courier)
        {
            courier = new DAMCourier(_courier);
        }
        [HttpGet]
        public VMResponse GetAll() => courier.GetByFilter();

        [HttpGet("[action]/{id?}")]
        public VMResponse Get(int id) => courier.GetById(id);

        [HttpGet("[action]/{filter?}")]
        public VMResponse GetBy(string filter) => courier.GetByFilter(filter);

        [HttpPost]
        public VMResponse Create(VMMCourier data) => courier.Create(data);
        [HttpPut]
        public VMResponse Update(VMMCourier data) => courier.Update(data);

        [HttpDelete]
        public VMResponse Delete(int id, int DeletedBy) => courier.Delete(id, DeletedBy);


    }
}
