using Cabana.Models.DB;
using NPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Security;

namespace Cabana.Statics
{
    public class UserHelper
    {
        public static MyUser Current()
        {
            var user = Membership.GetUser();
            var userId = (int)user.ProviderUserKey;

            IDatabase db = new Database("umbracoDbDSN");
            List<MyUser> _u = db.Fetch<MyUser>("where UmbId = " + userId);
            if (_u.IsNull())
                throw new Exception();
            if (_u.Count() == 0)
                throw new Exception();
            return _u[0];
        }

        public static List<MyUser> All()
        {
            var user = Membership.GetUser();
            var userId = (int)user.ProviderUserKey;

            IDatabase db = new Database("umbracoDbDSN");
            List<MyUser> _u = db.Fetch<MyUser>();
            if (_u.IsNull())
                throw new Exception();
            
            return _u.OrderBy(x=>x.Name).ToList();
        }
    }
}