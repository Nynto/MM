using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace MovieMakers.Models.ViewModels
{
    public class EventVm
    {
        public Event Event { get; set; }

        public IEnumerable<SelectListItem> MovieList { get; set; }
        public IEnumerable<SelectListItem> HallList { get; set; }
        // public IEnumerable<SelectListItem> StartTimeList { get; set; }



        // public static IEnumerable<Movie> Movies { get; set; }
        // public SelectList Genres { get; set; }
        // public string MovieGenre { get; set; }
        // public string SearchString { get; set; }
    }
}