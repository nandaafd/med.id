using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace BATCH336A.ViewModel
{
    public class VMMMenu
    {

        public long Id { get; set; }
        public string? Name { get; set; }
        public string? Url { get; set; }
        public long? ParentId { get; set; }
        public IFormFile? ImageFile { get; set; }
        public string? BigIcon { get; set; }
        public string? SmallIcon { get; set; }
        public long CreateBy { get; set; }
        public DateTime CreateOn { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public long? DeletedBy { get; set; }
        public DateTime? DeletedOn { get; set; }
        public bool IsDelete { get; set; }
        public List<VMMMenuRole>? Roles { get; set; }
    }
}
