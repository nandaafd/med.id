using System.ComponentModel.DataAnnotations;

namespace BATCH336A.ViewModel
{
    public class VMMMedicalFacilityCategory
    {
        public long? Id { get; set; }
        [Required]
        [MinLength(2)]
        public string? Name { get; set; }
        public long CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public long? DeletedBy { get; set; }
        public DateTime? DeletedOn { get; set; }
        public bool? IsDelete { get; set; }

    }
}