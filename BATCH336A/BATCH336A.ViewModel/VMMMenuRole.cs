using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BATCH336A.ViewModel
{
    public class VMMMenuRole
    {

        public int Id { get; set; }
        public long? MenuId { get; set; }
        public long? RoleId { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public long? DeletedBy { get; set; }
        public DateTime? DeletedOn { get; set; }
        public bool? IsDelete { get; set; }
    }
}
