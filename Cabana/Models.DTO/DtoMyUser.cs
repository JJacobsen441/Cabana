using System.Collections.Generic;

namespace Cabana.Models.DTO
{
    public class DtoMyUser
    {
        public DtoMyUser(long Id, int UmbId, string Name, List<DtoMovie> mov)
        {
            this.Id = Id;
            this.UmbId = UmbId;
            this.Name = Name;
            this.Movies = mov;
        }
        public long Id { get; set; }
        public int UmbId { get; set; }
        public string Name { get; set; }
        public List<DtoMovie> Movies{ get;set;}
        //public List<Movie> movies { get; set; }
    }/**/
}