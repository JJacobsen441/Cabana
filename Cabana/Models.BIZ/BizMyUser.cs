using Cabana.Models.DB;
using Cabana.Models.DTO;
using Cabana.Statics;
using NPoco;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Cabana.Models.BIZ
{
    public class BizMyUser
    {
        public void AddUser(string name, int umb_id)
        {
            if (name.IsNull())
                throw new Exception();

            using (IDatabase db = new Database("umbracoDbDSN"))
            { 
                MyUser u = new MyUser()
                {
                    Name = name,
                    UmbId = umb_id
                };
                db.Insert(u);
                //db.Dispose();
            }
        }

        public MyUser GetUser(string name)
        {
            if (name.IsNull())
                throw new Exception();

            using (IDatabase db = new Database("umbracoDbDSN"))
            {
                List<MyUser> _u = db.Fetch<MyUser>(string.Format("where MyUser.Name = '{0}'", name));
                if (_u.IsNull())
                    throw new Exception();

                if (_u.Any())
                    return _u[0];
                return new MyUser();
            }
        }

        public MyUser GetUserMovies(string name)
        {
            if (name.IsNull())
                throw new Exception();

            using (IDatabase db = new Database("umbracoDbDSN"))
            {
                List<MyUser> _u = db.FetchOneToMany<MyUser>(x=>x.Movies, string.Format("select u.*, m.* from MyUser u inner join Movie m on u.Id = m.MyUserId where u.Name = '{0}' order by u.Id", name));
                if (_u.IsNull())
                    throw new Exception();

                if (_u.Any())
                    return _u[0];
                return new MyUser();
            }
        }

        public DtoMyUser ToDTO(MyUser user)
        {
            if (user.IsNull())
                throw new Exception();

            BizMovie biz = new BizMovie();
            //we set this to an empty list instead of null, so it dosnt break in loops
            List<DtoMovie> list = new List<DtoMovie>();
            if(user.Movies != null && user.Movies.Any())
                list = biz.ToDTOList(user.Movies);

            DtoMyUser _u = new DtoMyUser(user.Id, user.UmbId, user.Name, list);
            return _u;
        }
    }
}