using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BATCH336A.DataModel
{
    [Keyless]
    public partial class VwMedicalFacility
    {
        [Column("doctor_id")]
        public long DoctorId { get; set; }
        [Column("name")]
        [StringLength(50)]
        [Unicode(false)]
        public string? Name { get; set; }
        [Column("medical_facility_id")]
        public long MedicalFacilityId { get; set; }
        [Column("alamatlengkap")]
        [Unicode(false)]
        public string Alamatlengkap { get; set; } = null!;
        [Column("price", TypeName = "decimal(18, 0)")]
        public decimal? Price { get; set; }
        [Column("biasa", TypeName = "decimal(18, 0)")]
        public decimal? Biasa { get; set; }
        [Column("specialization")]
        [StringLength(100)]
        [Unicode(false)]
        public string? Specialization { get; set; }
    }
}
