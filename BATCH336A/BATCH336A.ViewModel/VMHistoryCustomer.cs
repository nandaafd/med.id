using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BATCH336A.ViewModel
{
    public class VMMHistoryCustomer
    {
        public long? Id { get; set; }

        public long? AppointmentCustomerId { get; set; }
        public string? AppointmentCustomerName { get; set; }
        public DateTime? CustomerDob { get; set; }
        public string? CustomerGender { get; set; }
        public long? ParentBiodata { get; set; }
        public long? BiodataId { get; set; }


        public DateTime? AppointmentDate { get; set; }
        public long? AppointmentCreateBy { get; set; }
        public DateTime? AppointmentCreateOn { get; set; }

        public long? AppointmentDoctorOfficeId { get; set; }
        public string? MedicalFacilityName { get; set; }
        public string? DoctorName { get; set; }
        public string? DoctorSpecialization { get; set; }

        public long? AppointmentDoctorOfficeScheduleId { get; set; }

        public long? AppointmentDoctorOfficeTreatmentId { get; set; }
        public string? TreatmentName { get; set; }

        public long AppointmentDoneId { get; set; }
        public long? AppointmentId { get; set; }
        public string? Diagnosis { get; set; } = null!;
        public List<VMTPrescription>? Prescriptions { get; set; }
    }
}
