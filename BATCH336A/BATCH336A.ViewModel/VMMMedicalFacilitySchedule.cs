using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BATCH336A.ViewModel
{
    public class VMMMedicalFacilitySchedule
    {
        public long? doctorOfficeId { get; set; }
        public long? medicalFacilityId { get; set; }
        public long? doctorOfficeScheduleId { get; set; }
        public string? Day { get; set; }
        public string? TimeScheduleStart { get; set; }
        public string? TimeScheduleEnd { get; set; }
        public int? slot { get; set; }
    }
}
