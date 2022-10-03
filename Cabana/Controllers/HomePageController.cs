using Cabana.Models;
using System.Web.Mvc;
using Umbraco.Web.Mvc;

namespace Cabana.Controllers
{
    public class HomePageController : RenderMvcController
    {
        //public ActionResult HomePage()
        //{
        //    ColSearch _m = new ColSearch(CurrentPage, null);
            
        //    return View("HomePage", (ColSearch)_m);
        //}

        public ActionResult Index()
        {
            // you are in control here!

            // return a 'model' to the selected template/view for this page.
            return CurrentTemplate(CurrentPage);
        }
    }
}