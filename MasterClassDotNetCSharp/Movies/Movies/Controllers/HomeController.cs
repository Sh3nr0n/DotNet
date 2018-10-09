using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Movies.Models;
using Newtonsoft.Json;
using TMDbLib.Client;
using TMDbLib.Objects.General;
using TMDbLib.Objects.Movies;

namespace Movies.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            TMDbClient client = new TMDbClient("0464ee9cc3ccc27c18a7cbe6802c87c1");
            var movie = client.GetMovieNowPlayingListAsync("fr").Result;
            return View(movie.Results);
        }

        public ActionResult MovieDetail(int id)
        {
            TMDbClient client = new TMDbClient("0464ee9cc3ccc27c18a7cbe6802c87c1");
            Movie movie = client.GetMovieAsync(id, "fr").Result;
            ResultContainer<Video> videos = client.GetMovieVideosAsync(id).Result;
            return View(new MovieDetailViewModel
            {
                Movie = movie,
                Videos = videos.Results
            });
        }

        public ActionResult GetSimilarMovie(int id)
        {
            TMDbClient client = new TMDbClient("0464ee9cc3ccc27c18a7cbe6802c87c1");
            var similar = client.GetMovieSimilarAsync(id, "fr").Result;
            return PartialView("_SimilarMovies",similar.Results);
        }

        public ActionResult GetVideo(int id)
        {
            TMDbClient client = new TMDbClient("0464ee9cc3ccc27c18a7cbe6802c87c1");
            ResultContainer<Video> videos = client.GetMovieVideosAsync(id).Result;
            return PartialView("_Video", videos.Results);
        }

        public ActionResult Serie()
        {
            TMDbClient client = new TMDbClient("0464ee9cc3ccc27c18a7cbe6802c87c1");
            var seriez = client.GetTvShowTopRatedAsync(language: "fr").Result;
            return View(seriez.Results);
        }

        public ActionResult SerieDetail(int id, string language)
        {
            TMDbClient client = new TMDbClient("0464ee9cc3ccc27c18a7cbe6802c87c1");
            var seriez = client.GetTvShowAsync(id, language: "fr").Result;
            return View(seriez);

        }

    }
}