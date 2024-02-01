using BATCH336A.DataAccess;
using BATCH336A.DataModel;
using BATCH336A.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace BATCH336A.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PenarikanSaldoController : Controller
    {
        private DAPenarikanSaldo penarikansaldo;
        public PenarikanSaldoController(BATCH336AContext _db) 
        {
            penarikansaldo = new DAPenarikanSaldo(_db);
        }

        [HttpGet("[action]/{id?}")]
        public VMResponse GetById(long id) => penarikansaldo.GetById(id);

        [HttpGet]
        public VMResponse GetDefNom() => penarikansaldo.GetDefNomId();

        [HttpGet("[action]/{id?}")]
        public VMResponse GetCustNom(long id) => penarikansaldo.GetCustNomId(id);

        [HttpPut("[action]")]
        public VMResponse CekPin(VMPenarikanSaldo data) => penarikansaldo.CekPin(data);
        [HttpPut]
        public VMResponse UpdateTrans(VMPenarikanSaldo data) => penarikansaldo.Transaksi(data);
    }

}
