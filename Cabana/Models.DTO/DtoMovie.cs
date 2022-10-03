using System.Collections.Generic;

namespace Cabana.Models.DTO
{
    public class DtoMovie
    {
        public DtoMovie(long id, string title, double votes_avg, List<string> genres, string desc)
        {
            this.movie_id = id;
            this.title = title;
            this.votes_avg = votes_avg;
            this.genres = genres;
            this.description = desc;
        }
        public long movie_id { get; set; }
        public string title { get; set; }
        public double votes_avg { get; set; }
        public List<string> genres { get; set; }
        public string description { get; set; }
    }
}