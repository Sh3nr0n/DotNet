using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TMDbLib.Client;

namespace Movies.Controllers
{
    public class SearchController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Search(string search)
        {
            if (string.IsNullOrEmpty(search))
            {
                return null;
            }

            TMDbClient client = new TMDbClient("0464ee9cc3ccc27c18a7cbe6802c87c1");
            return PartialView("_Resultat", client.SearchMovieAsync(search,"fr").Result.Results) ;
        }
 
    }
}