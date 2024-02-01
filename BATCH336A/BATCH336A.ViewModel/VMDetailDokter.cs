using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BATCH336A.ViewModel
{
    public class VMDetailDokter
    {
        //profile 
        public string? DoctorName { get; set; }
        public string? DoctorSpecialization { get; set; }
        public int? Pengalaman { get; set; }
        public string? Image { get; set; }


        //education
        public string? InstitutionName { get; set; }
        public string? Major { get; set; }
        public string? EndYear { get; set; }

        //tindakan medis
        public string? TreatmentName { get; set; }

        //riwayat praktek
        public string? FacilityName { get; set; }
        public DateTime? StartYear { get; set; }
        public DateTime? EndIsYear { get; set; }
        public string? LocationName { get; set; }
        public string? Specialization { get; set; }

        //lokasi dan jadwal praktek
        public string? NameMF { get; set; }
        public string? FullAddress { get; set; }
        public decimal? Price { get; set; }
        public string? Schedule { get; set; }
        public string? StartJam { get; set; }
        public string? EndJam { get; set; }
        public string? Day { get; set; }
        public long? IdMf { get; set; }
        public decimal? OnlinePrice { get; set; }












    }
}
