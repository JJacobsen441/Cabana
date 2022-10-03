using Cabana.Models;
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
    public class ShowController : RenderMvcController
    {
        public ActionResult Index(ContentModel model, int movie_id)
        {
            // you are in control here!

            try
            {
                Movie mov = RestHelper.MovieGET(movie_id);
                
                List<string> genres = new List<string>();
                foreach (Genre _g in mov.genres)
                    genres.Add(_g.name);

                IPublishedContent content = CurrentPage;
                DtoMovie movie = new DtoMovie(mov.id, mov.title, mov.vote_average, genres, mov.tagline);

                ColShow col = new ColShow(content, movie);

                return View("Show", (ColShow)col);
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
        }
    }
}