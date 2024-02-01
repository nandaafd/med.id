using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BATCH336A.ViewModel
{
    public class VMMPendaftaran
    {
        public VMMBiodatum Biodatum { get; set; }
        public VMMUser UserData { get; set; }
    }
}
