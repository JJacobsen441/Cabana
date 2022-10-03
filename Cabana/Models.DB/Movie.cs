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
        
        public long MyUserID { get; set; }

        //public MyUser user { get; set; }
    }
}