using Microsoft.AspNetCore.Authorization;
using MovieMakers.DataAccess.Repository.IRepository;
using MovieMakers.Models;
using Microsoft.AspNetCore.Mvc;
using MovieMakers.Utility;

namespace BulkyBook.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class RowController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public RowController(IUnitOfWork unitOfWork)
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
            Row row = new Row();
            if (id == null)
            {
                return View(row);
            }

            row = _unitOfWork.Row.Get(id.GetValueOrDefault());
            if (row == null)
            {
                return NotFound();
            }
            return View(row);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Row row)
        {
            if (ModelState.IsValid)
            {
                if (row.Id == 0)
                {
                    _unitOfWork.Row.Add(row);
                }
                else
                {
                    _unitOfWork.Row.Update(row);

                    var seatsToBeRemoved = _unitOfWork.Seat.GetAll(s => s.RowId == row.Id);
                    _unitOfWork.Seat.RemoveRange(seatsToBeRemoved);

                    var i = 1;
                    while (i <= row.NumberOfSeats)
                    {
                        Seat seat = new Seat
                        {
                            RowId = row.Id,
                            Number = i
                        };

                        _unitOfWork.Seat.Add(seat);

                        i++;
                    }
                }
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(row);
        }
        
        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
        {
            var allObj = _unitOfWork.Row.GetAll(includeProperties: "Hall");
            return Json(new {data = allObj});
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var objFromDb = _unitOfWork.Row.Get(id);
            if (objFromDb == null)
            {
                return Json(new {success = false, message = "Error while deleting"});
            }
            _unitOfWork.Row.Remove(objFromDb);
            _unitOfWork.Save();
            return Json(new {success = true, message = "Delete Successful"});
        }
        
        #endregion
    }
}