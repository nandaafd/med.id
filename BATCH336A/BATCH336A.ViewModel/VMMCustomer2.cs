using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BATCH336A.ViewModel
{
    public class VMMCustomer2
    {
        public long Id { get; set; }
        public long? BiodataId { get; set; }
        public string? Name { get; set; }
        public DateTime? Dob { get; set; }
        public string? Gender { get; set; }
        public long? BloodGroupId { get; set; }
        public string? RhesusType { get; set; }
        public decimal? Height { get; set; }
        public decimal? Weight { get; set; }
        public long CreateBy { get; set; }
        public DateTime CreateOn { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public long? DeletedBy { get; set; }
        public DateTime? DeletedOn { get; set; }
        public bool IsDelete { get; set; }

        public List<VMMCustomerMember>? Members { get; set; }
    }
}
