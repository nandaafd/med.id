using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BATCH336A.ViewModel
{
    public class VMCariDokter
    {
        public long? id { get; set; }
        public string? doctorName { get; set; }
        public int? pengalaman { get; set; }
        public string? specialization { get; set; }
        public string? ImagePath { get; set; }
        public List<VMLokasiCariDokter>? Locations { get; set; }
    }
}
