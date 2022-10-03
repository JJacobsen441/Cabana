using Cabana.Models;
using Cabana.Models.DB;
using Cabana.Models.DTO;
using Cabana.Statics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Umbraco.Web.WebApi;

namespace Cabana.Controllers
{
    
    //[Route("api")]
    public class ApiAdminController : UmbracoAuthorizedApiController
    {
        public class Result
        {
            public string name { get; set; }
        }

        [HttpGet]
        //[Route("members")]
        public string GetAllMembers()
        {
            /*
             * I would have liked to return something like JsonResult, but UmbracoAuthorizedApiController dosnt inherit from Controller
             * */
            try
            {
                List<MyUser> users = UserHelper.All();
                List<string> names = users.Select(x => x.Name).ToList();
            
                string res = "{\"members\":[";
                int _c = 0;
                foreach (string entry in names)
                {
                    if (_c != 0 && _c < names.Count())
                        res += ",";
                    res += "{\"name\":\"" + entry + "\"}";
                    _c++;
                }
                res += "], \"error\":\"\"}";

                return res;/**/
            }
            catch (Exception e)
            {
                //Response.StatusCode = 500;
                return "{\"error\":\"error\"}";
            }
        }

        [HttpGet]
        //[Route("member/{name}/movies")]
        //[Route("member/{name?}/movies")]
        //[Route("umbraco/backoffice/api/{apiadmin}/{member}/{name?}/{movies}")]
        public string GetMembersMovies(string name)
        {
            try
            {
                if (name.IsNull())
                    throw new Exception();

                if (!CheckHelper.CheckName(ref name, false, 20, true, new List<string>() { "no_tag" }, CharacterHelper.All(false)))
                    throw new Exception();

                string res, _res;
                DtoMyUser user = DBAccess.GetUser(name);

                if (string.IsNullOrEmpty(user.Name))
                    _res = "{\"error\":\"no users by that name\"}";
                else
                {
                    List<DtoMovie> movies = DBAccess.GetMovies(user.Id);

                    _res = "{\"name\":\"" + user.Name + "\"," + 
                        "\"movies\":[";
                    int c = 0;
                    foreach (DtoMovie entry in movies)
                    {
                        if (c != 0)
                            _res += ",";
                        _res += "{\"m_name\":\"" + entry.title + "\"}";
                        c++;
                    }
                    _res += "], \"error\":\"\"}";
                }
                res = _res;                

                return res;/**/
            }
            catch (Exception e)
            {
                //Response.StatusCode = 500;
                return "{\"error\":\"error\"}";
            }
        }

        /*[Route("product/{id?}")]
        public string GetMember(int? id)
        {
            if (id != null)
            {
                return $"Monitor model {id}";
            }
            return "Base model Monitor";
        }/**/
    }
}
