using System.ComponentModel.DataAnnotations;

namespace Cabana.Models
{
    public class VmSearch
    {
        [Required]
        public bool genres { get; set; }

        [Required]
        public bool votes { get; set; }

        [Required]
        public string search_string { get; set; }
    }
}
