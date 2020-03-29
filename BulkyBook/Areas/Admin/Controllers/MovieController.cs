using System;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using MovieMakers.DataAccess.Repository.IRepository;
using MovieMakers.Models;
using MovieMakers.Models.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MovieMakers.Utility;

namespace BulkyBook.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class MovieController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostEnvironment;

        public MovieController(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostEnvironment = hostEnvironment;
        }
        // GET
        public IActionResult Index()
        {
            return View();
        }
        
        public IActionResult Upsert(int? id)
        {
            MovieVm movieVm = new MovieVm()
            {
                Movie = new Movie(),
                GenreList = _unitOfWork.Genre.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                }),
                AgeGroupList = _unitOfWork.AgeGroup.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                })
            };
            if (id == null)
            {
                return View(movieVm);
            }

            movieVm.Movie = _unitOfWork.Movie.Get(id.GetValueOrDefault());
            if (movieVm.Movie == null)
            {
                return NotFound();
            }
            return View(movieVm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(MovieVm movieVm)
        {
            if (ModelState.IsValid)
            {
                string webRootPath = _hostEnvironment.WebRootPath;
                var files = HttpContext.Request.Form.Files;
                if (files.Count > 0)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(webRootPath, @"images/products");
                    var extention = Path.GetExtension(files[0].FileName);
                    if (movieVm.Movie.ImageUrl != null)
                    {
                        var imagePath = Path.Combine(webRootPath, movieVm.Movie.ImageUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(imagePath))
                        {
                            System.IO.File.Delete(imagePath);
                        }
                    }

                    using (var fileStreams =
                        new FileStream(Path.Combine(uploads, fileName + extention), FileMode.Create))
                    {
                        files[0].CopyTo(fileStreams);
                    }

                    movieVm.Movie.ImageUrl = @"\images/products/" + fileName + extention;
                }
                else
                {
                    ///update when they do nog change the image
                    if (movieVm.Movie.Id != 0)
                    {
                        Movie objFromDb = _unitOfWork.Movie.Get(movieVm.Movie.Id);
                        movieVm.Movie.ImageUrl = objFromDb.ImageUrl;
                    }
                }
                if (movieVm.Movie.Id == 0)
                {
                    _unitOfWork.Movie.Add(movieVm.Movie);
                }
                else
                {
                    _unitOfWork.Movie.Update(movieVm.Movie);
                }
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            else
            {

                movieVm.GenreList = _unitOfWork.Genre.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                });

                movieVm.AgeGroupList = _unitOfWork.AgeGroup.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                });
                if (movieVm.Movie.Id != 0)
                {
                    movieVm.Movie = _unitOfWork.Movie.Get(movieVm.Movie.Id);

                }

            }
            return View(movieVm);
        }
        
        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
        {
            var allObj = _unitOfWork.Movie.GetAll(includeProperties:"Genre,AgeGroup");
            return Json(new {data = allObj});
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var objFromDb = _unitOfWork.Movie.Get(id);
            if (objFromDb == null)
            {
                return Json(new {success = false, message = "Error while deleting"});
            }
            string webRootPath = _hostEnvironment.WebRootPath;
            var imagePath = Path.Combine(webRootPath, objFromDb.ImageUrl.TrimStart('\\'));
            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
            }
            _unitOfWork.Movie.Remove(objFromDb);
            _unitOfWork.Save();
            return Json(new {success = true, message = "Delete Successful"});
        }
        
        #endregion
    }
}