using Cabana.Models;
using Cabana.Models.DB;
using Cabana.Models.DTO;
using Cabana.Statics;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using Umbraco.Web.Mvc;
using Umbraco.Web.WebApi;

namespace Cabana.Controllers
{

    //[Route("api")]
    //[IsBackOffice]
    //[PluginController("myapi")]
    public class ApiAdminController : UmbracoAuthorizedApiController
    {
        /*
         * not my code
         * */
        public class JsonHttpStatusResult<T> : JsonResult<T>
        {
            private readonly HttpStatusCode _httpStatus;

            public JsonHttpStatusResult(T content, ApiController controller, HttpStatusCode httpStatus)
            : base(content, new JsonSerializerSettings(), new UTF8Encoding(), controller)
            {
                _httpStatus = httpStatus;
            }

            public override Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
            {
                var returnTask = base.ExecuteAsync(cancellationToken);
                returnTask.Result.StatusCode = _httpStatus;// HttpStatusCode.BadRequest;
                return returnTask;
            }
        }

        [System.Web.Mvc.HttpGet]
        //[Route("members")]
        public JsonResult<Result1> GetAllMembers()
        {
            /*
             * I would have liked to return something like JsonResult, but UmbracoAuthorizedApiController dosnt inherit from Controller
             * ..found a way :)
             * ..It seems now it is only routing that is missing, but I wont go further into that
             * */

            try
            {
                Result1 res = new Result1();
                res.members = new List<Entry1>();
                res.Error = "";

                List<MyUser> users = UserHelper.All();
                if(users.Any())
                {                               
                    foreach (MyUser user in users)
                        res.members.Add(new Entry1() { name = user.Name });

                    return new JsonHttpStatusResult<Result1>(res, this, HttpStatusCode.OK);
                }

                res.members = new List<Entry1>();
                res.Error = "no users found";
                return new JsonHttpStatusResult<Result1>(res, this, HttpStatusCode.OK);
            }
            catch (Exception e)
            {
                Result1 res = new Result1() { members = new List<Entry1>(), Error = "internal error" };
                return new JsonHttpStatusResult<Result1>(res, this, HttpStatusCode.InternalServerError);
            }
        }

        [System.Web.Mvc.HttpGet]
        //[Route("member/{name}/movies")]
        //[Route("member/{name?}/movies")]
        //[Route("umbraco/backoffice/api/{apiadmin}/{member}/{name?}/{movies}")]
        public JsonResult<Result2> GetMembersMovies(string name)
        {
            /*
             * I would have liked to return something like JsonResult, but UmbracoAuthorizedApiController dosnt inherit from Controller
             * ..found a way :)
             * ..It seems now it is only routing that is missing, but I wont go further into that
             * */

            try
            {
                Result2 res = new Result2();
                res.name = "";
                res.movies = new List<Entry2>();
                res.Error = "";

                if (CheckHelper.CheckName(ref name, false, 20, true, new List<string>() { "no_tag" }, CharacterHelper.All(false)))
                {
                    DtoMyUser user = DBAccess.GetUserMovies(name);

                    if (!string.IsNullOrEmpty(user.Name))
                    {
                        res.name = user.Name;

                        foreach (DtoMovie mov in user.Movies)
                            res.movies.Add(new Entry2() { m_name = mov.title });

                        if (!res.movies.Any())
                            res.Error = "no saved movies";

                        return new JsonHttpStatusResult<Result2>(res, this, HttpStatusCode.OK);
                    }
                }
                
                res.name = "";
                res.movies = new List<Entry2>();
                res.Error = "no users by that name";

                return new JsonHttpStatusResult<Result2>(res, this, HttpStatusCode.OK);
            }
            catch (Exception e)
            {
                Result2 res = new Result2() { name = "", movies = new List<Entry2>(), Error = "internal error" };
                return new JsonHttpStatusResult<Result2>(res, this, HttpStatusCode.InternalServerError);
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
