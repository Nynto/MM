using MovieMakers.DataAccess.Repository.IRepository;
using MovieMakers.Models;
using MovieMakers.Utility;
using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBook.Areas.Admin.Controllers
{

        [Area("Admin")]
        [Authorize(Roles = SD.Role_Admin)]
        public class AgeGroupController : Controller
        {
            private readonly IUnitOfWork _unitOfWork;

            public AgeGroupController(IUnitOfWork unitOfWork)
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
                AgeGroup ageGroup = new AgeGroup();
                if (id == null)
                {
                    return View(ageGroup);
                }

                ageGroup = _unitOfWork.AgeGroup.Get(id.GetValueOrDefault());
                if (ageGroup == null)
                {
                    return NotFound();
                }
                return View(ageGroup);
        }

            [HttpPost]
            [ValidateAntiForgeryToken]
            public IActionResult Upsert(AgeGroup ageGroup)
            {
                if (ModelState.IsValid)
                {
                    if (ageGroup.Id == 0)
                    {
                        _unitOfWork.AgeGroup.Add(ageGroup);

                    }
                    else
                    {
                    _unitOfWork.AgeGroup.Update(ageGroup);
                    }

                    _unitOfWork.Save();
                    return RedirectToAction(nameof(Index));
                }

                return View(ageGroup);
            }

        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
        {
            var allObj = _unitOfWork.AgeGroup.GetAll();
            return Json(new { data = allObj });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var objFromDb = _unitOfWork.AgeGroup.Get(id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
            _unitOfWork.AgeGroup.Remove(objFromDb);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete Successful" });
        }

        #endregion
    }

}
