using Cabana.Models;
using Cabana.Models.DTO;
using Cabana.Statics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web.Mvc;

namespace Cabana.Controllers
{
    public class CabanaController : SurfaceController
    {
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Search(VmSearch model)
        {
            try
            {
                if (!ModelState.IsValid)
                    throw new Exception();

                if (!CheckHelper.CheckSearch(model))
                    throw new Exception();

                Movies mov = RestHelper.MoviesGET(model.search_string);
                Genres gen = RestHelper.GenresGET();

                List<DtoMovie> _m = new List<DtoMovie>();
                foreach (Result _r in mov.results)
                {
                    /*
                     * we deliver all genres, even though we dont use all genres in front
                     * 
                     * could be done like this, oneliner
                     * _r.genre_ids.ForEach(x => genres.Add(gen.genres.Where(z => z.id == x).FirstOrDefault().name));
                     * */
                    List<string> genres = new List<string>();
                    foreach (int _i in _r.genre_ids)
                    {
                        Genre _g = gen.genres.Where(x => x.id == _i).FirstOrDefault();
                        genres.Add(_g.name);
                    }

                    _m.Add(new DtoMovie(_r.id, _r.title, _r.vote_average, genres, null));
                }

                _m =    model.genres ?  _m.OrderBy(x => x.genres.Count() == 0 ? "" : x.genres[0]).ToList() :
                        model.votes  ?  _m.OrderByDescending(x => x.votes_avg).ToList() :
                                        _m.OrderBy(x => x.title).ToList();

                IPublishedContent content = CurrentPage;
                IEnumerable<DtoMovie> movies = _m;

                ColSearch col = new ColSearch(content, movies);

                return View("Search", (ColSearch)col);
            }
            catch (Exception _e)
            {
                var _root = Umbraco.ContentAtRoot().First();
                var fail = _root.Children.Where(x => x.ContentType.Alias == "fail").First();
                int failPageId = fail.Id;

                return RedirectToUmbracoPage(failPageId);
            }
        }        
    }
}
