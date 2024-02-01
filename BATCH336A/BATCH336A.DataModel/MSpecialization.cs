using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BATCH336A.DataModel
{
    [Table("m_specialization")]
    public partial class MSpecialization
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }
        [Column("name")]
        [StringLength(50)]
        [Unicode(false)]
        public string? Name { get; set; }
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
        [InverseProperty("MSpecializationCreatedByNavigations")]
        public virtual MUser CreatedByNavigation { get; set; } = null!;
        [ForeignKey("DeletedBy")]
        [InverseProperty("MSpecializationDeletedByNavigations")]
        public virtual MUser? DeletedByNavigation { get; set; }
        [ForeignKey("ModifiedBy")]
        [InverseProperty("MSpecializationModifiedByNavigations")]
        public virtual MUser? ModifiedByNavigation { get; set; }
    }
}
