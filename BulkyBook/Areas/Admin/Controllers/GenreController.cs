using Microsoft.AspNetCore.Authorization;
using MovieMakers.DataAccess.Repository.IRepository;
using MovieMakers.Models;
using Microsoft.AspNetCore.Mvc;
using MovieMakers.Utility;

namespace BulkyBook.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class GenreController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public GenreController(IUnitOfWork unitOfWork)
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
            Genre genre = new Genre();
            if (id == null)
            {
                return View(genre);
            }

            genre = _unitOfWork.Genre.Get(id.GetValueOrDefault());
            if (genre == null)
            {
                return NotFound();
            }
            return View(genre);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Genre genre)
        {
            if (ModelState.IsValid)
            {
                if (genre.Id == 0)
                {
                    _unitOfWork.Genre.Add(genre);

                }
                else
                {
                    _unitOfWork.Genre.Update(genre);
                }
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(genre);
        }
        
        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
        {
            var allObj = _unitOfWork.Genre.GetAll();
            return Json(new {data = allObj});
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var objFromDb = _unitOfWork.Genre.Get(id);
            if (objFromDb == null)
            {
                return Json(new {success = false, message = "Error while deleting"});
            }
            _unitOfWork.Genre.Remove(objFromDb);
            _unitOfWork.Save();
            return Json(new {success = true, message = "Delete Successful"});
        }
        
        #endregion
    }
}