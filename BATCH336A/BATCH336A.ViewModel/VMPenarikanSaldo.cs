using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BATCH336A.ViewModel
{
    public class VMPenarikanSaldo
    {
        public long? CustWalletId { get; set; }
        public long? CustId { get; set; }
        public string? Pin { get; set; }
        public int? cekPin { get; set; }
        public long? NomCustId { get; set; }
        public long? NominalCustom { get; set; }
        public long? NomDefId { get; set; }
        public decimal? DefaultNominal { get; set; }
        public decimal? Saldo { get; set; }
        public int? Otp { get; set; }
        public long? Amount { get; set; }
        public string? BankName { get; set; }
        public long? Transaksi { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }

    }
}
