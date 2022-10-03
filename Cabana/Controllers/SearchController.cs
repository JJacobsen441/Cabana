using Cabana.Models;
using System.Web.Mvc;
using Umbraco.Web.Mvc;

namespace Cabana.Controllers
{
    public class SearchController : RenderMvcController
    {
        public ActionResult Search()
        {
            ColSearch _m = new ColSearch(CurrentPage, null);

            return View("Search", (ColSearch)_m);
        }

        //public override ActionResult Index()
        //{
        //    // you are in control here!

        //    // return a 'model' to the selected template/view for this page.
        //    return CurrentTemplate(CurrentPage);
        //}
    }
}