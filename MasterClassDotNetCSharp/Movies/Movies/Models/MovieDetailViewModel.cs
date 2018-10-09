using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TMDbLib.Objects.General;
using TMDbLib.Objects.Movies;

namespace Movies.Models
{
    public class MovieDetailViewModel
    {
        public Movie Movie { get; set; }

        public List<Video> Videos { get; set; }
    }
}