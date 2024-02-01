using BATCH336A.DataAccess;
using BATCH336A.DataModel;
using BATCH336A.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BATCH336A.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistoryCustomerController : ControllerBase
    {
        private DAHistoryCustomer historyCustomer;
        public HistoryCustomerController(BATCH336AContext _db) 
        {
            historyCustomer = new DAHistoryCustomer(_db);
        }
        [HttpGet("[action]/GetAllById/{id?}")]
        public VMResponse GetAll(long? id) => historyCustomer.GetAll(id);
        [HttpGet("[action]/{filter?}")]
        public VMResponse GetByFilter(string filter) => historyCustomer.GetByFilter(filter);
        [HttpGet("[action]/{id?}")]
        public VMResponse GetPrescriptionById(int id) => historyCustomer.GetPrescriptionById(id);
        [HttpGet("[action]/{id?}")]
        public VMResponse GetById(int id) => historyCustomer.GetById(id);
        [HttpPut]
        public VMResponse Edit(VMTPrescription data) => historyCustomer.Update(data);
    }
}
