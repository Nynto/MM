using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace MovieMakers.Models.ViewModels
{
    public class ReservationVm
    {
        public Reservation Reservation { get; set; }

        public IEnumerable<SelectListItem> EventList { get; set; }
        public IEnumerable<SelectListItem> SeatList { get; set; }
    }
}