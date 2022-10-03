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

            IDatabase db = new Database("umbracoDbDSN");
            MyUser u = new MyUser()
            {
                Name = name,
                UmbId = umb_id
            };
            db.Insert(u);
            db.Dispose();
        }

        public MyUser GetUser(string name)
        {
            if (name.IsNull())
                throw new Exception();

            IDatabase db = new Database("umbracoDbDSN");
            List<MyUser> _u = db.Fetch<MyUser>(string.Format("where MyUser.Name = '{0}'", name));

            if (_u.IsNull())
                throw new Exception();

            if (_u.Any())
                return _u[0];
            return new MyUser();
        }

        public DtoMyUser ToDTO(MyUser user)
        {
            DtoMyUser _u = new DtoMyUser(user.Id, user.UmbId, user.Name);
            return _u;
        }
    }
}