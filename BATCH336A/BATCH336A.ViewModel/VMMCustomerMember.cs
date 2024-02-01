using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BATCH336A.ViewModel
{
    public class VMMCustomerMember
    {
        public int Id { get; set; }
        public long? ParentBiodataId { get; set; }
        public long? CustomerId { get; set; }
        public string? CustomerName { get; set; }
        public long? CustomerRelationId { get; set; }
        public string? CustomerRelationName { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public long? DeletedBy { get; set; }
        public DateTime? DeletedOn { get; set; }
        public bool? IsDelete { get; set; }
    }
}
