using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BATCH336A.ViewModel
{
    public class VMTAppointmentDetails
    {
        public long? Id { get; set; }

        public long? AppointmentCustomerId { get; set; }
        public string? AppointmentCustomerName {  get; set; }

        public DateTime? AppointmentDate { get; set; }

        public long? AppointmentDoctorOfficeId { get; set; }
        public string? MedicalFacilityName {  get; set; }
        public string? DoctorName {  get; set; }

        public long? AppointmentDoctorOfficeScheduleId { get; set; }

        public long? AppointmentDoctorOfficeTreatmentId { get; set; }
        public string? TreatmentName { get; set; }

        public long AppointmentDoneId { get; set; }
        public long? AppointmentId { get; set; }
        public string Diagnosis { get; set; } = null!;
        public List<VMTPrescription>? Prescriptions { get; set; }
    }
}
