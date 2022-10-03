using Cabana.Models.DB;
using Cabana.Statics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using Umbraco.Core;
using Umbraco.Core.Cache;
using Umbraco.Core.Logging;
using Umbraco.Core.Persistence;
using Umbraco.Core.Services;
using Umbraco.Web.Models;
using Umbraco.Web.Mvc;

namespace Umbraco.Web.Controllers
{
    public class RegisterController : SurfaceController  //to avoid confusion we change the name of the class - in this example we changed it from UmbRegisterController to UmbAlternativeRegisterController
    {
        public RegisterController()    //the class name has to be changed in the constructors as well
        {
        }

        public RegisterController(IUmbracoContextAccessor umbracoContextAccessor, IUmbracoDatabaseFactory databaseFactory, ServiceContext services, AppCaches appCaches, ILogger logger, IProfilingLogger profilingLogger, UmbracoHelper umbracoHelper)
            : base(umbracoContextAccessor, databaseFactory, services, appCaches, logger, profilingLogger, umbracoHelper)
        {
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateUmbracoFormRouteString]
        public ActionResult HandleRegisterMember([Bind(Prefix = "registerModel")] RegisterModel model, string memberGroup)  //on top of the original controller code we add the memberGroup parameter
        {
            try
            {
                if (ModelState.IsValid == false)
                {
                    return CurrentUmbracoPage();
                }

                if (string.IsNullOrEmpty(model.Name) && string.IsNullOrEmpty(model.Email) == false)
                {
                    model.Name = model.Email;
                }







                //my code
                string name = model.Name;
                string email = model.Email;
                if (!CheckHelper.CheckName(ref name, false, 20, true, new List<string>() { "no_tag" }, CharacterHelper.All(false)))
                    throw new Exception();

                if (!CheckHelper.IsValidEmail(email))
                    throw new Exception();






                MembershipCreateStatus status;
                var member = Members.RegisterMember(model, out status, model.LoginOnSuccess);

                switch (status)
                {
                    case MembershipCreateStatus.Success:

                        TempData["FormSuccess"] = true;
                        AssignMemberGroup(model.Email, memberGroup);

                        /*using (DBContext db = new DBContext())
                        {
                            MyUser _u = new MyUser();
                            _u.Name = model.Name;
                            db.myuser.Add(_u);
                            db.SaveChanges();
                        }/**/

                        //my code
                        int userId = (int)member.ProviderUserKey;
                        DBAccess.AddUser(name, userId);                    

                        if (model.RedirectUrl.IsNullOrWhiteSpace() == false)
                        {
                            return Redirect(model.RedirectUrl);
                        }

                        return RedirectToCurrentUmbracoPage();
                    case MembershipCreateStatus.InvalidUserName:
                        ModelState.AddModelError((model.UsernameIsEmail || model.Username == null)
                            ? "registerModel.Email"
                            : "registerModel.Username",
                            "Username is not valid");
                        break;
                    case MembershipCreateStatus.InvalidPassword:
                        ModelState.AddModelError("registerModel.Password", "The password is not strong enough");
                        break;
                    case MembershipCreateStatus.InvalidQuestion:
                    case MembershipCreateStatus.InvalidAnswer:
                        throw new NotImplementedException(status.ToString());
                    case MembershipCreateStatus.InvalidEmail:
                        ModelState.AddModelError("registerModel.Email", "Email is invalid");
                        break;
                    case MembershipCreateStatus.DuplicateUserName:
                        ModelState.AddModelError((model.UsernameIsEmail || model.Username == null)
                            ? "registerModel.Email"
                            : "registerModel.Username",
                            "A member with this username already exists.");
                        break;
                    case MembershipCreateStatus.DuplicateEmail:
                        ModelState.AddModelError("registerModel.Email", "A member with this e-mail address already exists");
                        break;
                    case MembershipCreateStatus.UserRejected:
                    case MembershipCreateStatus.InvalidProviderUserKey:
                    case MembershipCreateStatus.DuplicateProviderUserKey:
                    case MembershipCreateStatus.ProviderError:
                        ModelState.AddModelError("registerModel", "An error occurred creating the member: " + status);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                return CurrentUmbracoPage();
            }
            catch (Exception _e)
            {
                var _root = Umbraco.ContentAtRoot().First();
                var fail = _root.Children.Where(x => x.ContentType.Alias == "fail").First();
                int failPageId = fail.Id;

                return RedirectToUmbracoPage(failPageId);
            }
        }

        //the below method will allow us to assign the member to an already existing group
        private void AssignMemberGroup(string email, string group)
        {
            try
            {
                Services.MemberService.AssignRole(email, group);
            }
            catch (Exception ex)
            {
                //handle the exception
            }

        }

    }
}