using Cabana.Models.DB;
using Cabana.Models.DTO;
using Cabana.Statics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Cabana.Models.BIZ
{
    public class BizMovie
    {
        public bool HasMovie(long userId, int movie_id)
        {
            using (DBContext db = new DBContext())
            {
                IQueryable<Cabana.Models.DB.Movie> _m = db.movie.Where(x => x.MyUserID == userId && x.MovieID == movie_id);
                Cabana.Models.DB.Movie movie = _m.FirstOrDefault();

                if (!movie.IsNull())
                    return true;
                return false;
                //db.Dispose();
            }

            /*using (IDatabase db = new Database("umbracoDbDSN"))
            { 
                List<Cabana.Models.DB.Movie> movies = db.Fetch<Cabana.Models.DB.Movie>("where MyUserId = " + userId + " and MovieID = " + movie_id);


                if (movies.IsNull())
                    throw new Exception();

                if (movies.Any())
                    return true;
                return false;
                //db.Dispose();
            }/**/
        }

        public List<Cabana.Models.DB.Movie> GetMovies(long userId)
        {
            using (DBContext db = new DBContext())
            {
                IQueryable<Cabana.Models.DB.Movie> _m = db.movie.Where(x => x.MyUserID == userId);
                List<Cabana.Models.DB.Movie> movies = _m.ToList();
                if (movies.IsNull())
                    throw new Exception();

                return movies;
                //db.Dispose();
            }

            /*using (IDatabase db = new Database("umbracoDbDSN"))
            {
                List<Cabana.Models.DB.Movie> movies = db.Fetch<Cabana.Models.DB.Movie>("where MyUserId = " + userId);
                if (movies.IsNull())
                    throw new Exception();

                return movies;
                //db.Dispose();
            }/**/
        }

        public void AddMovie(int movie_id)
        {
            MyUser _u = UserHelper.Current();

            if (HasMovie(_u.Id, movie_id))
                return;

            Cabana.Models.Movie mov = RestHelper.MovieGET(movie_id);

            using (DBContext db = new DBContext())
            {
                Cabana.Models.DB.Movie _m = new Cabana.Models.DB.Movie()
                {
                    MovieID = mov.id,
                    Title = mov.title,
                    MyUserID = _u.Id
                };
                db.movie.Add(_m);
                db.SaveChanges();
                //db.Dispose();
            }

            /*using (IDatabase db = new Database("umbracoDbDSN"))
            {
                Cabana.Models.DB.Movie _m = new Cabana.Models.DB.Movie()
                {
                    MovieID = mov.id,
                    Title = mov.title,
                    MyUserID = _u.Id
                };
                db.Insert<Cabana.Models.DB.Movie>(_m);
                //db.Dispose();
            }/**/
        }

        public void DeleteMovie(int movie_id)
        {
            MyUser _u = UserHelper.Current();

            if (!HasMovie(_u.Id, movie_id))
                return;

            using (DBContext db = new DBContext())
            {
                IQueryable<Cabana.Models.DB.Movie> _m = db.movie.Where(x => x.MovieID == movie_id && x.MyUserID == _u.Id);
                Cabana.Models.DB.Movie mov = _m.FirstOrDefault();
                
                db.movie.Remove(mov);
                db.SaveChanges();
                //db.Dispose();
            }

            /*using (IDatabase db = new Database("umbracoDbDSN"))
            {
                var mov = db.Fetch<Cabana.Models.DB.Movie>("where MovieId = " + movie_id + " and MyUserId = " + _u.Id);
                db.Delete("Movie", "Id", mov[0]);
                //db.Dispose();
            }/**/
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