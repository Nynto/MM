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
                var parameter = new DynamicParameters();
                parameter.Add("@Id", id);
                ageGroup = _unitOfWork.SP_Call.OneRecord<AgeGroup>(SD.Proc_CoverType_Get, parameter);
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
                    var parameter = new DynamicParameters();
                    parameter.Add("@Name", ageGroup.Name);
                    if (ageGroup.Id == 0)
                    {
                        _unitOfWork.SP_Call.Execute(SD.Proc_CoverType_Create, parameter );

                    }
                    else
                    {
                        parameter.Add("@Id", ageGroup.Id);
                        _unitOfWork.SP_Call.Execute(SD.Proc_CoverType_Update, parameter );
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
                var allObj = _unitOfWork.SP_Call.List<AgeGroup>(SD.Proc_CoverType_GetAll, null);
                return Json(new {data = allObj});
            }

            [HttpDelete]
            public IActionResult Delete(int id)
            {
                var parameter = new DynamicParameters();
                parameter.Add("@Id", id);
                var objFromDb = _unitOfWork.SP_Call.OneRecord<AgeGroup>(SD.Proc_CoverType_Get, parameter);
                if (objFromDb == null)
                {
                    return Json(new {success = false, message = "Error while deleting"});
                }

                _unitOfWork.SP_Call.Execute(SD.Proc_CoverType_Delete, parameter);
                _unitOfWork.Save();
                return Json(new {success = true, message = "Delete Successful"});
            }

            #endregion
        }


    }
