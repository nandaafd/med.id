using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BATCH336A.DataModel
{
    [Keyless]
    public partial class VwProfileDoctor
    {
        [Column("name")]
        [StringLength(50)]
        [Unicode(false)]
        public string? Name { get; set; }
        [Column("fullname")]
        [StringLength(255)]
        [Unicode(false)]
        public string? Fullname { get; set; }
        [Column("image_path")]
        [StringLength(255)]
        [Unicode(false)]
        public string? ImagePath { get; set; }
        [Column("pengalaman")]
        public int? Pengalaman { get; set; }
    }
}
