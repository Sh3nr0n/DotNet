// Model used by the controller to display the view
using TMDbLib.Objects.General;
using TMDbLib.Objects.Movies;

namespace Movies.Models
{
    public class MovieDetailViewModel
    {
        public Movie Movie { get; set; }
    }
}