using System;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using MovieMakers.DataAccess.Repository.IRepository;
using MovieMakers.Models;
using MovieMakers.Models.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MovieMakers.Utility;

namespace BulkyBook.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class EventController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public EventController(IUnitOfWork unitOfWork)
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
            EventVm eventVm = new EventVm()
            {
                Event = new Event(),
                MovieList = _unitOfWork.Movie.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Title,
                    Value = i.Id.ToString()
                }),
                HallList = _unitOfWork.Hall.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                })
            };
            if (id == null)
            {
                return View(eventVm);
            }

            eventVm.Event = _unitOfWork.Event.Get(id.GetValueOrDefault());
            if (eventVm.Event == null)
            {
                return NotFound();
            }

            return View(eventVm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(EventVm eventVm)
        {
            if (ModelState.IsValid)
            {
                if (eventVm.Event.Id == 0)
                {
                    _unitOfWork.Event.Add(eventVm.Event);
                }
                else
                {
                    _unitOfWork.Event.Update(eventVm.Event);
                }
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                eventVm.MovieList = _unitOfWork.Movie.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Title,
                    Value = i.Id.ToString()
                });

                eventVm.HallList = _unitOfWork.Hall.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                });

                if (eventVm.Event.Id != 0)
                {
                    eventVm.Event = _unitOfWork.Event.Get(eventVm.Event.Id);
                }
            }

            return View(eventVm);
        }

        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
        {
            var allObj = _unitOfWork.Event.GetAll(includeProperties: "Movie,Hall");
            return Json(new {data = allObj});
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var objFromDb = _unitOfWork.Event.Get(id);
            if (objFromDb == null)
            {
                return Json(new {success = false, message = "Error while deleting"});
            }

            _unitOfWork.Event.Remove(objFromDb);
            _unitOfWork.Save();
            return Json(new {success = true, message = "Delete Successful"});
        }

        #endregion
    }
}