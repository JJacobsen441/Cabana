using Cabana.Models;
using Cabana.Models.DB;
using Cabana.Models.DTO;
using Cabana.Statics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Results;
using System.Web.Mvc;
using Umbraco.Web.WebApi;

namespace Cabana.Controllers
{
    
    //[Route("api")]
    public class ApiAdminController : UmbracoAuthorizedApiController
    {
        

        [HttpGet]
        //[Route("members")]
        public JsonResult<Result1> GetAllMembers()
        {
            /*
             * I would have liked to return something like JsonResult, but UmbracoAuthorizedApiController dosnt inherit from Controller
             * ..found a way :)
             * */

            try
            {
                List<MyUser> users = UserHelper.All();
                List<string> names = users.Select(x => x.Name).ToList();
                                
                Result1 res = new Result1();
                res.members = new List<Entry1>();
                foreach (string entry in names)
                    res.members.Add(new Entry1() { name = entry });
                res.Error = "";
                
                return Json(res);/**/
            }
            catch (Exception e)
            {
                //Response.StatusCode = 500;
                Result1 res = new Result1() { Error = "error" };
                return Json(res);
            }
        }

        [HttpGet]
        //[Route("member/{name}/movies")]
        //[Route("member/{name?}/movies")]
        //[Route("umbraco/backoffice/api/{apiadmin}/{member}/{name?}/{movies}")]
        public JsonResult<Result2> GetMembersMovies(string name)
        {
            /*
             * I would have liked to return something like JsonResult, but UmbracoAuthorizedApiController dosnt inherit from Controller
             * ..found a way :)
             * */

            try
            {
                if (name.IsNull())
                    throw new Exception();

                if (!CheckHelper.CheckName(ref name, false, 20, true, new List<string>() { "no_tag" }, CharacterHelper.All(false)))
                    throw new Exception();

                Result2 res = new Result2();
                DtoMyUser user = DBAccess.GetUserMovies(name);

                if (string.IsNullOrEmpty(user.Name))
                    res.Error = "no users by that name or user has no movies";
                else
                {
                    List<DtoMovie> movies = user.Movies;

                    res.name = user.Name;
                    res.movies = new List<Entry2>();

                    foreach (DtoMovie entry in movies)
                        res.movies.Add(new Entry2() { m_name = entry.title });
                        
                    res.Error = "";
                }
                
                return Json(res);
            }
            catch (Exception e)
            {
                //Response.StatusCode = 500;
                Result2 res = new Result2() { Error = "error" };
                return Json(res);
            }
        }

        /*[HttpGet]
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

                return res;/**
            }
            catch (Exception e)
            {
                //Response.StatusCode = 500;
                return "{\"error\":\"error\"}";
            }
        }/**/

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
