using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BATCH336A.DataModel
{
    [Table("t_doctor_office_treatment_price")]
    public partial class TDoctorOfficeTreatmentPrice
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }
        [Column("doctor_office_treatment_id")]
        public long? DoctorOfficeTreatmentId { get; set; }
        [Column("price", TypeName = "decimal(18, 0)")]
        public decimal? Price { get; set; }
        [Column("price_start_from", TypeName = "decimal(18, 0)")]
        public decimal? PriceStartFrom { get; set; }
        [Column("price_until_form", TypeName = "decimal(18, 0)")]
        public decimal? PriceUntilForm { get; set; }
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
        [InverseProperty("TDoctorOfficeTreatmentPriceCreatedByNavigations")]
        public virtual MUser CreatedByNavigation { get; set; } = null!;
        [ForeignKey("DeletedBy")]
        [InverseProperty("TDoctorOfficeTreatmentPriceDeletedByNavigations")]
        public virtual MUser? DeletedByNavigation { get; set; }
        [ForeignKey("ModifiedBy")]
        [InverseProperty("TDoctorOfficeTreatmentPriceModifiedByNavigations")]
        public virtual MUser? ModifiedByNavigation { get; set; }
    }
}
