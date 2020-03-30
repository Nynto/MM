using Microsoft.AspNetCore.Authorization;
using MovieMakers.DataAccess.Repository.IRepository;
using MovieMakers.Models;
using Microsoft.AspNetCore.Mvc;
using MovieMakers.Utility;
using MovieMakers.Models.ViewModels;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BulkyBook.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin + "," + SD.Role_FO_Employee)]
    public class ReservationController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ReservationController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET
        public IActionResult Index()
        {
            return View();
        }
        
        public IActionResult Upsert(int? id)
        {
            ReservationVm reservationVm = new ReservationVm()
            {
                Reservation = new Reservation(),
                EventList = _unitOfWork.Event.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Date + " " + i.StartTime + " " + i.Movie.Title,
                    Value = i.Id.ToString()
                }),
                SeatList = _unitOfWork.Seat.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Row.Hall.Name + " " + i.Row.Number + " " + i.Number,
                    Value = i.Id.ToString()
                })
            };

            if (id == null)
            {
                return View(reservationVm);
            }

            reservationVm.Reservation = _unitOfWork.Reservation.Get(id.GetValueOrDefault());
            if (reservationVm.Reservation == null)
            {
                return NotFound();
            }
            return View(reservationVm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(ReservationVm reservationVm)
        {
            if (ModelState.IsValid)
            {
                if (reservationVm.Reservation.Id == 0)
                {
                    _unitOfWork.Reservation.Add(reservationVm.Reservation);
                }
                else
                {
                    _unitOfWork.Reservation.Update(reservationVm.Reservation);
                }
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                reservationVm.EventList = _unitOfWork.Event.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Date + " " + i.StartTime + " " + i.Movie.Title,
                    Value = i.Id.ToString()
                });

                reservationVm.SeatList = _unitOfWork.Seat.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Row.Hall.Name + " " + i.Row.Number + " " + i.Number,
                    Value = i.Id.ToString()
                });

                if (reservationVm.Reservation.Id != 0)
                {
                    reservationVm.Reservation = _unitOfWork.Reservation.Get(reservationVm.Reservation.Id);
                }
            }
            return View(reservationVm);
        }
        
        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
        {
            var allObj = _unitOfWork.Reservation.GetAll(includeProperties: "Event, Seat");
            return Json(new {data = allObj});
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var objFromDb = _unitOfWork.Reservation.Get(id);
            if (objFromDb == null)
            {
                return Json(new {success = false, message = "Error while deleting"});
            }

            _unitOfWork.Reservation.Remove(objFromDb);
            _unitOfWork.Save();
            return Json(new {success = true, message = "Delete Successful"});
        }
        
        #endregion
    }
}