using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cabana.Models.DB
{
    [Table("Movie")]
    public class Movie
    {
        [Key]
        public int Id { get; set; }

        public long MovieID { get; set; }

        public string Title { get; set; }

        [Required]
        public long MyUserID { get; set; }

        //[Required]
        public MyUser MyUser { get; set; }
    }
}