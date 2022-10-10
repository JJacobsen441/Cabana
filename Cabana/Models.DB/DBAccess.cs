using Cabana.Models.BIZ;
using Cabana.Models.DTO;
using System.Collections.Generic;

namespace Cabana.Models.DB
{
    public class DBAccess
    {
        public static List<DtoMovie> GetMovies(int userId)
        {
            BizMovie biz = new BizMovie();
            List<Cabana.Models.DB.Movie> movies = biz.GetMovies(userId);
            
            return biz.ToDTOList(movies);
        }

        public static void AddMovie(int movie_id)
        {
            BizMovie biz = new BizMovie();
            biz.AddMovie(movie_id);
        }

        public static void DeleteMovie(int movie_id)
        {
            BizMovie biz = new BizMovie();
            biz.DeleteMovie(movie_id);
        }

        public static void AddUser(string name, int umb_id)
        {
            BizMyUser biz = new BizMyUser();
            biz.AddUser(name, umb_id);
        }

        public static DtoMyUser GetUser(string name)
        {
            BizMyUser biz = new BizMyUser();
            MyUser user = biz.GetUser(name);

            return biz.ToDTO(user);
        }

        public static DtoMyUser GetUserMovies(string name)
        {
            BizMyUser biz = new BizMyUser();
            MyUser user = biz.GetUserMovies(name);

            return biz.ToDTO(user);
        }
    }
}