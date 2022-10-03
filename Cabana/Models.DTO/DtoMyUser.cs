namespace Cabana.Models.DTO
{
    public class DtoMyUser
    {
        public DtoMyUser(int Id, int UmbId, string Name) 
        {
            this.Id = Id;
            this.UmbId = UmbId;
            this.Name = Name;
        }
        public int Id { get; set; }
        public int UmbId { get; set; }
        public string Name { get; set; }
        //public List<Movie> movies { get; set; }
    }/**/
}