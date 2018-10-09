// The controller uses the model to display a view in the layout

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication5.Models;

//Add following to use the TMDbLib library in our app

using TMDbLib.Client;
using TMDbLib.Objects.General;
using TMDbLib.Objects.Movies;
using Movies.Models;

namespace WebApplication5.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            TMDbClient client = new TMDbClient ("0464ee9cc3ccc27c18a7cbe6802c87c1"); // Create a new TMDB Client to access the API using the API key
            var movie = client.GetMovieNowPlayingListAsync("fr").Result; // Method from the API to request a list of movie from the online database
            return View(movie.Results); // return the view
        }
        public ActionResult MovieDetail(int id){
            TMDbClient client = new TMDbClient("0464ee9cc3ccc27c18a7cbe6802c87c1");
            Movie movie = client.GetMovieAsync(id, "fr").Result;
            return View(new MovieDetailViewModel
            {
                Movie = movie,
            });
        }

    }
}
