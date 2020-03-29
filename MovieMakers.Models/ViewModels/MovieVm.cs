using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace MovieMakers.Models.ViewModels
{
    public class MovieVm
    {
        public Movie Movie { get; set; }
        public IEnumerable<SelectListItem> GenreList { get; set; }
        public IEnumerable<SelectListItem> AgeGroupList { get; set; }
        // public static IEnumerable<Movie> Movies { get; set; }
        // public SelectList Genres { get; set; }
        // public string MovieGenre { get; set; }
        // public string SearchString { get; set; }
    }
}