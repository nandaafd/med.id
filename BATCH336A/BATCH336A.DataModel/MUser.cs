using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BATCH336A.DataModel
{
    [Table("m_user")]
    public partial class MUser
    {
        public MUser()
        {
            MLocationLevelCreatedByNavigations = new HashSet<MLocationLevel>();
            MLocationLevelDeletedByNavigations = new HashSet<MLocationLevel>();
            MLocationLevelModifiedByNavigations = new HashSet<MLocationLevel>();
            MMedicalFacilityCategoryCreatedByNavigations = new HashSet<MMedicalFacilityCategory>();
            MMedicalFacilityCategoryDeletedByNavigations = new HashSet<MMedicalFacilityCategory>();
            MMedicalFacilityCategoryModifiedByNavigations = new HashSet<MMedicalFacilityCategory>();
            MMedicalFacilityCreatedByNavigations = new HashSet<MMedicalFacility>();
            MMedicalFacilityDeletedByNavigations = new HashSet<MMedicalFacility>();
            MMedicalFacilityModifiedByNavigations = new HashSet<MMedicalFacility>();
            MMedicalFacilityScheduleCreatedByNavigations = new HashSet<MMedicalFacilitySchedule>();
            MMedicalFacilityScheduleDeletedByNavigations = new HashSet<MMedicalFacilitySchedule>();
            MMedicalFacilityScheduleModifiedByNavigations = new HashSet<MMedicalFacilitySchedule>();
            MSpecializationCreatedByNavigations = new HashSet<MSpecialization>();
            MSpecializationDeletedByNavigations = new HashSet<MSpecialization>();
            MSpecializationModifiedByNavigations = new HashSet<MSpecialization>();
            TCurrentDoctorSpecializationCreatedByNavigations = new HashSet<TCurrentDoctorSpecialization>();
            TCurrentDoctorSpecializationDeletedByNavigations = new HashSet<TCurrentDoctorSpecialization>();
            TCurrentDoctorSpecializationModifiedByNavigations = new HashSet<TCurrentDoctorSpecialization>();
            TDoctorOfficeCreatedByNavigations = new HashSet<TDoctorOffice>();
            TDoctorOfficeDeletedByNavigations = new HashSet<TDoctorOffice>();
            TDoctorOfficeModifiedByNavigations = new HashSet<TDoctorOffice>();
            TDoctorOfficeTreatmentCreatedByNavigations = new HashSet<TDoctorOfficeTreatment>();
            TDoctorOfficeTreatmentDeletedByNavigations = new HashSet<TDoctorOfficeTreatment>();
            TDoctorOfficeTreatmentModifiedByNavigations = new HashSet<TDoctorOfficeTreatment>();
            TDoctorOfficeTreatmentPriceCreatedByNavigations = new HashSet<TDoctorOfficeTreatmentPrice>();
            TDoctorOfficeTreatmentPriceDeletedByNavigations = new HashSet<TDoctorOfficeTreatmentPrice>();
            TDoctorOfficeTreatmentPriceModifiedByNavigations = new HashSet<TDoctorOfficeTreatmentPrice>();
            TDoctorTreatmentCreatedByNavigations = new HashSet<TDoctorTreatment>();
            TDoctorTreatmentDeletedByNavigations = new HashSet<TDoctorTreatment>();
            TDoctorTreatmentModifiedByNavigations = new HashSet<TDoctorTreatment>();
        }

        [Key]
        [Column("id")]
        public long Id { get; set; }
        [Column("biodata_id")]
        public long? BiodataId { get; set; }
        [Column("role_id")]
        public long? RoleId { get; set; }
        [Column("email")]
        [StringLength(100)]
        [Unicode(false)]
        public string? Email { get; set; }
        [Column("password")]
        [StringLength(255)]
        [Unicode(false)]
        public string? Password { get; set; }
        [Column("login_attempt")]
        public int? LoginAttempt { get; set; }
        [Column("is_locked")]
        public bool? IsLocked { get; set; }
        [Column("last_login", TypeName = "datetime")]
        public DateTime? LastLogin { get; set; }
        [Column("create_by")]
        public long CreateBy { get; set; }
        [Column("create_on", TypeName = "datetime")]
        public DateTime CreateOn { get; set; }
        [Column("modified_by")]
        public long? ModifiedBy { get; set; }
        [Column("modified_on", TypeName = "datetime")]
        public DateTime? ModifiedOn { get; set; }
        [Column("deleted_by")]
        public long? DeletedBy { get; set; }
        [Column("deleted_on", TypeName = "date")]
        public DateTime? DeletedOn { get; set; }
        [Column("is_delete")]
        public bool IsDelete { get; set; }

        [InverseProperty("CreatedByNavigation")]
        public virtual ICollection<MLocationLevel> MLocationLevelCreatedByNavigations { get; set; }
        [InverseProperty("DeletedByNavigation")]
        public virtual ICollection<MLocationLevel> MLocationLevelDeletedByNavigations { get; set; }
        [InverseProperty("ModifiedByNavigation")]
        public virtual ICollection<MLocationLevel> MLocationLevelModifiedByNavigations { get; set; }
        [InverseProperty("CreatedByNavigation")]
        public virtual ICollection<MMedicalFacilityCategory> MMedicalFacilityCategoryCreatedByNavigations { get; set; }
        [InverseProperty("DeletedByNavigation")]
        public virtual ICollection<MMedicalFacilityCategory> MMedicalFacilityCategoryDeletedByNavigations { get; set; }
        [InverseProperty("ModifiedByNavigation")]
        public virtual ICollection<MMedicalFacilityCategory> MMedicalFacilityCategoryModifiedByNavigations { get; set; }
        [InverseProperty("CreatedByNavigation")]
        public virtual ICollection<MMedicalFacility> MMedicalFacilityCreatedByNavigations { get; set; }
        [InverseProperty("DeletedByNavigation")]
        public virtual ICollection<MMedicalFacility> MMedicalFacilityDeletedByNavigations { get; set; }
        [InverseProperty("ModifiedByNavigation")]
        public virtual ICollection<MMedicalFacility> MMedicalFacilityModifiedByNavigations { get; set; }
        [InverseProperty("CreatedByNavigation")]
        public virtual ICollection<MMedicalFacilitySchedule> MMedicalFacilityScheduleCreatedByNavigations { get; set; }
        [InverseProperty("DeletedByNavigation")]
        public virtual ICollection<MMedicalFacilitySchedule> MMedicalFacilityScheduleDeletedByNavigations { get; set; }
        [InverseProperty("ModifiedByNavigation")]
        public virtual ICollection<MMedicalFacilitySchedule> MMedicalFacilityScheduleModifiedByNavigations { get; set; }
        [InverseProperty("CreatedByNavigation")]
        public virtual ICollection<MSpecialization> MSpecializationCreatedByNavigations { get; set; }
        [InverseProperty("DeletedByNavigation")]
        public virtual ICollection<MSpecialization> MSpecializationDeletedByNavigations { get; set; }
        [InverseProperty("ModifiedByNavigation")]
        public virtual ICollection<MSpecialization> MSpecializationModifiedByNavigations { get; set; }
        [InverseProperty("CreatedByNavigation")]
        public virtual ICollection<TCurrentDoctorSpecialization> TCurrentDoctorSpecializationCreatedByNavigations { get; set; }
        [InverseProperty("DeletedByNavigation")]
        public virtual ICollection<TCurrentDoctorSpecialization> TCurrentDoctorSpecializationDeletedByNavigations { get; set; }
        [InverseProperty("ModifiedByNavigation")]
        public virtual ICollection<TCurrentDoctorSpecialization> TCurrentDoctorSpecializationModifiedByNavigations { get; set; }
        [InverseProperty("CreatedByNavigation")]
        public virtual ICollection<TDoctorOffice> TDoctorOfficeCreatedByNavigations { get; set; }
        [InverseProperty("DeletedByNavigation")]
        public virtual ICollection<TDoctorOffice> TDoctorOfficeDeletedByNavigations { get; set; }
        [InverseProperty("ModifiedByNavigation")]
        public virtual ICollection<TDoctorOffice> TDoctorOfficeModifiedByNavigations { get; set; }
        [InverseProperty("CreatedByNavigation")]
        public virtual ICollection<TDoctorOfficeTreatment> TDoctorOfficeTreatmentCreatedByNavigations { get; set; }
        [InverseProperty("DeletedByNavigation")]
        public virtual ICollection<TDoctorOfficeTreatment> TDoctorOfficeTreatmentDeletedByNavigations { get; set; }
        [InverseProperty("ModifiedByNavigation")]
        public virtual ICollection<TDoctorOfficeTreatment> TDoctorOfficeTreatmentModifiedByNavigations { get; set; }
        [InverseProperty("CreatedByNavigation")]
        public virtual ICollection<TDoctorOfficeTreatmentPrice> TDoctorOfficeTreatmentPriceCreatedByNavigations { get; set; }
        [InverseProperty("DeletedByNavigation")]
        public virtual ICollection<TDoctorOfficeTreatmentPrice> TDoctorOfficeTreatmentPriceDeletedByNavigations { get; set; }
        [InverseProperty("ModifiedByNavigation")]
        public virtual ICollection<TDoctorOfficeTreatmentPrice> TDoctorOfficeTreatmentPriceModifiedByNavigations { get; set; }
        [InverseProperty("CreatedByNavigation")]
        public virtual ICollection<TDoctorTreatment> TDoctorTreatmentCreatedByNavigations { get; set; }
        [InverseProperty("DeletedByNavigation")]
        public virtual ICollection<TDoctorTreatment> TDoctorTreatmentDeletedByNavigations { get; set; }
        [InverseProperty("ModifiedByNavigation")]
        public virtual ICollection<TDoctorTreatment> TDoctorTreatmentModifiedByNavigations { get; set; }
    }
}
