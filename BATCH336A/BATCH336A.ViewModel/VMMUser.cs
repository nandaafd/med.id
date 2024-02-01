using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BATCH336A.ViewModel
{
    public class VMMUser
    {

        public long Id { get; set; }
        public long? BiodataId { get; set; }
        public string? BiodataName { get; set; }
        public long? RoleId { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public int? LoginAttempt { get; set; }
        public bool? IsLocked { get; set; }
        public DateTime? LastLogin { get; set; }
        public long CreateBy { get; set; }
        public DateTime CreateOn { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public long? DeletedBy { get; set; }
        public DateTime? DeletedOn { get; set; }
        public bool IsDelete { get; set; }
    }
}
