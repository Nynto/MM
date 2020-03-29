using Microsoft.AspNetCore.Authorization;
using MovieMakers.DataAccess.Repository.IRepository;
using MovieMakers.Models;
using Microsoft.AspNetCore.Mvc;
using MovieMakers.Utility;

namespace BulkyBook.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin + "," + SD.Role_FO_Employee)]
    public class LostAndFoundController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public LostAndFoundController(IUnitOfWork unitOfWork)
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
            LostAndFound lostAndFound = new LostAndFound();
            if (id == null)
            {
                return View(lostAndFound);
            }

            lostAndFound = _unitOfWork.LostAndFound.Get(id.GetValueOrDefault());
            if (lostAndFound == null)
            {
                return NotFound();
            }
            return View(lostAndFound);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(LostAndFound lostAndFound)
        {
            if (ModelState.IsValid)
            {
                if (lostAndFound.Id == 0)
                {
                    _unitOfWork.LostAndFound.Add(lostAndFound);

                }
                else
                {
                    _unitOfWork.LostAndFound.Update(lostAndFound);
                }
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(lostAndFound);
        }
        
        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
        {
            var allObj = _unitOfWork.LostAndFound.GetAll();
            return Json(new {data = allObj});
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var objFromDb = _unitOfWork.LostAndFound.Get(id);
            if (objFromDb == null)
            {
                return Json(new {success = false, message = "Error while deleting"});
            }
            _unitOfWork.LostAndFound.Remove(objFromDb);
            _unitOfWork.Save();
            return Json(new {success = true, message = "Delete Successful"});
        }
        
        #endregion
    }
}