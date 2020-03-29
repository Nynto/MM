using Microsoft.AspNetCore.Authorization;
using MovieMakers.DataAccess.Repository.IRepository;
using MovieMakers.Models;
using Microsoft.AspNetCore.Mvc;
using MovieMakers.Utility;

namespace BulkyBook.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class HallController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public HallController(IUnitOfWork unitOfWork)
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
            Hall hall = new Hall();
            if (id == null)
            {
                return View(hall);
            }

            hall = _unitOfWork.Hall.Get(id.GetValueOrDefault());
            if (hall == null)
            {
                return NotFound();
            }
            return View(hall);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Hall hall)
        {
            if (ModelState.IsValid)
            {
                if (hall.Id == 0)
                {
                    _unitOfWork.Hall.Add(hall);

                }
                else
                {
                    _unitOfWork.Hall.Update(hall);
                }
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(hall);
        }
        
        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
        {
            var allObj = _unitOfWork.Hall.GetAll();
            return Json(new {data = allObj});
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var objFromDb = _unitOfWork.Hall.Get(id);
            if (objFromDb == null)
            {
                return Json(new {success = false, message = "Error while deleting"});
            }
            _unitOfWork.Hall.Remove(objFromDb);
            _unitOfWork.Save();
            return Json(new {success = true, message = "Delete Successful"});
        }
        
        #endregion
    }
}