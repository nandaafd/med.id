using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BATCH336A.ViewModel
{
    public class VMMTindakan
    {
        public long? doctorOfficeId { get; set; }
        public long? medicalFacilityId { get; set; }
        public long? doctorOfficeTreatmentId { get; set; }
        public string? treatment {  get; set; }
    }
}
