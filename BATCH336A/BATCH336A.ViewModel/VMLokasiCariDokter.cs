using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BATCH336A.ViewModel
{
    public class VMLokasiCariDokter
    {
        public long? doctorOfficeId {  get; set; }
        public long? medicalFacilityId { get; set; }
        public long? idDokter {  get; set; }
        public int? lamaBekerja { get; set; }
        public DateTime? startDate { get; set; }
        public DateTime? endDate { get; set; }
        public int? startYear { get; set; }
        public int? endYear { get; set; }
        public string? namaLokasi {  get; set; }
        public string? namaRumahSakit { get; set; }
        public long? kategoriFaskesId { get; set; }

        public List<VMMMedicalFacilitySchedule>? jamOperasional {  get; set; }
        public List<VMMTindakan>? namaTreatment { get; set; }
}
}
