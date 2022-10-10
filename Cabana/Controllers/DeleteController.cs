using Cabana.Models;
using Cabana.Models.DB;
using Cabana.Models.DTO;
using Cabana.Statics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web;
using Umbraco.Web.Models;
using Umbraco.Web.Mvc;

namespace Cabana.Controllers
{
    public class DeleteController : RenderMvcController
    {
        public ActionResult Delete(ContentModel model, int? movie_id)
        {
            try
            {
                //something seems not rigth here!
                if (movie_id.IsNull())
                    return Index(model);
                                
                MyUser _u = UserHelper.Current();

                int id = (int)movie_id;
                DBAccess.DeleteMovie(id);
                
                var _root = Umbraco.ContentAtRoot().First();
                var save = _root.Children.Where(x => x.ContentType.Alias == "save").First();
                int savePageId = save.Id;

                var redirectPage = Umbraco.Content(savePageId); //page id here

                return Redirect(redirectPage.Url());
            }
            catch (Exception _e)
            {
                var _root = Umbraco.ContentAtRoot().First();
                var fail = _root.Children.Where(x => x.ContentType.Alias == "fail").First();
                int failPageId = fail.Id;

                var redirectPage = Umbraco.Content(failPageId); //page id here

                return Redirect(redirectPage.Url());
            }
        }
        
        /*public ActionResult Index(ContentModel model)
        {
            // you are in control here!

            try
            {
                IPublishedContent content = CurrentPage;

                MyUser _u = UserHelper.Current();

                List<DtoMovie> movies = DBAccess.GetMovies(_u.Id);

                ColSave col = new ColSave(content, movies);

                return View("Save", (ColSave)col);
            }
            catch (Exception _e)
            {
                var _root = Umbraco.ContentAtRoot().First();
                var fail = _root.Children.Where(x => x.ContentType.Alias == "fail").First();
                int failPageId = fail.Id;

                var redirectPage = Umbraco.Content(failPageId); //page id here

                return Redirect(redirectPage.Url());
            }

            // return a 'model' to the selected template/view for this page.
            //return CurrentTemplate(CurrentPage);
        }/**/
    }
}