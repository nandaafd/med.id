using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BATCH336A.DataModel
{
    [Table("m_medical_facility")]
    public partial class MMedicalFacility
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }
        [Column("name")]
        [StringLength(50)]
        [Unicode(false)]
        public string? Name { get; set; }
        [Column("medical_facility_category_id")]
        public long? MedicalFacilityCategoryId { get; set; }
        [Column("location_id")]
        public long? LocationId { get; set; }
        [Column("full_adress")]
        [Unicode(false)]
        public string? FullAdress { get; set; }
        [Column("email")]
        [StringLength(100)]
        [Unicode(false)]
        public string? Email { get; set; }
        [Column("phone_code")]
        [StringLength(10)]
        [Unicode(false)]
        public string? PhoneCode { get; set; }
        [Column("phone")]
        [StringLength(15)]
        [Unicode(false)]
        public string? Phone { get; set; }
        [Column("fax")]
        [StringLength(15)]
        [Unicode(false)]
        public string? Fax { get; set; }
        [Column("created_by")]
        public long CreatedBy { get; set; }
        [Column("created_on", TypeName = "datetime")]
        public DateTime CreatedOn { get; set; }
        [Column("modified_by")]
        public long? ModifiedBy { get; set; }
        [Column("modified_on", TypeName = "datetime")]
        public DateTime? ModifiedOn { get; set; }
        [Column("deleted_by")]
        public long? DeletedBy { get; set; }
        [Column("deleted_on", TypeName = "datetime")]
        public DateTime? DeletedOn { get; set; }
        [Column("is_delete")]
        public bool IsDelete { get; set; }

        [ForeignKey("CreatedBy")]
        [InverseProperty("MMedicalFacilityCreatedByNavigations")]
        public virtual MUser CreatedByNavigation { get; set; } = null!;
        [ForeignKey("DeletedBy")]
        [InverseProperty("MMedicalFacilityDeletedByNavigations")]
        public virtual MUser? DeletedByNavigation { get; set; }
        [ForeignKey("ModifiedBy")]
        [InverseProperty("MMedicalFacilityModifiedByNavigations")]
        public virtual MUser? ModifiedByNavigation { get; set; }
    }
}
