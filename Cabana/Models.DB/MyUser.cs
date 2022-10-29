using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cabana.Models.DB
{
    [Table("MyUser")]
    public class MyUser
    {
        [Key]
        public long Id { get; set; }
        public int UmbId { get; set; }
        public string Name { get; set; }
        [Required]
        public List<Movie> Movies { get; set; }
    }/**/

    //public class UserNPoco
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}