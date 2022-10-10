using Cabana.Models.DB;
using Cabana.Models.DTO;
using Cabana.Statics;
using NPoco;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Cabana.Models.BIZ
{
    public class BizMovie
    {
        public bool HasMovie(int userId, int movie_id)
        {
            using (IDatabase db = new Database("umbracoDbDSN"))
            { 
                List<Cabana.Models.DB.Movie> movies = db.Fetch<Cabana.Models.DB.Movie>("where MyUserId = " + userId + " and MovieID = " + movie_id);


                if (movies.IsNull())
                    throw new Exception();

                if (movies.Any())
                    return true;
                return false;
                //db.Dispose();
            }
        }

        public List<Cabana.Models.DB.Movie> GetMovies(int userId)
        {
            using (IDatabase db = new Database("umbracoDbDSN"))
            { 
                List<Cabana.Models.DB.Movie> movies = db.Fetch<Cabana.Models.DB.Movie>("where MyUserId = " + userId);
                if (movies.IsNull())
                    throw new Exception();

                return movies;
                //db.Dispose();
            }
        }

        public void AddMovie(int movie_id)
        {
            MyUser _u = UserHelper.Current();

            if (HasMovie(_u.Id, movie_id))
                return;

            Cabana.Models.Movie mov = RestHelper.MovieGET(movie_id);

            using (IDatabase db = new Database("umbracoDbDSN"))
            {
                Cabana.Models.DB.Movie _m = new Cabana.Models.DB.Movie()
                {
                    MovieID = mov.id,
                    Title = mov.title,
                    MyUserID = _u.Id
                };
                db.Insert<Cabana.Models.DB.Movie>(_m);
                //db.Dispose();
            }
        }

        public void DeleteMovie(int movie_id)
        {
            MyUser _u = UserHelper.Current();

            if (!HasMovie(_u.Id, movie_id))
                return;

            using (IDatabase db = new Database("umbracoDbDSN"))
            {
                var mov = db.Fetch<Cabana.Models.DB.Movie>("where MovieId = " + movie_id);
                db.Delete("Movie", "Id", mov[0]);
                //db.Dispose();
            }
        }

        public List<DtoMovie> ToDTOList(List<Cabana.Models.DB.Movie> movies)
        {
            if (movies.IsNull())
                throw new Exception();

            List<DtoMovie> _m = new List<DtoMovie>();
            foreach (Cabana.Models.DB.Movie mov in movies)
                _m.Add(new DtoMovie(mov.MovieID, mov.Title, -1, null, null));
            _m = _m.OrderBy(x => x.title).ToList();

            return _m;
        }
    }
}