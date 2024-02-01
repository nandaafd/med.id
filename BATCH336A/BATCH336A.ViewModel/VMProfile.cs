using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BATCH336A.ViewModel
{
    public class VMProfile
    {
        public long Id { get; set; }
        public long  UserId { get; set; }
        public string? Fullname { get; set; }
        public string? MobilePhone { get; set; }
        public byte[]? Image { get; set; }
        public string? ImagePath { get; set; }
        public long? BiodataId { get; set; }
        public long? RoleId { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public int? LoginAttempt { get; set; }
        public bool? IsLocked { get; set; }
        public DateTime? LastLogin { get; set; }
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

        public VMMBiodatum Biodata { get; set; }

        // Additional properties for Customer
        public VMMCustomer Customer { get; set; }

    }
}
