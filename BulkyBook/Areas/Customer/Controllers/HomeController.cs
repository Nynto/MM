using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

using BulkyBook.Areas.Admin.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using MovieMakers.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Extensions.Logging;
using MovieMakers.DataAccess.Data;
using MovieMakers.DataAccess.Repository;
using MovieMakers.Models;
using MovieMakers.Models.ViewModels;
using MovieMakers.Utility;

namespace BulkyBook.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index(string searchString)
        {

            IEnumerable<Event> eventList = _unitOfWork.Event.GetAll(includeProperties: "Movie,Hall");
            IEnumerable<Movie> movieList = _unitOfWork.Movie.GetAll(includeProperties: "Genre,AgeGroup");


          
            if (!string.IsNullOrEmpty(searchString))
            {
                movieList = movieList.Where(s => s.Title.Contains(searchString, StringComparison.OrdinalIgnoreCase));
                return View( movieList.ToList());
            }

            var claimsIdentity = (ClaimsIdentity) User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            if (claim != null)
            {
                var count = _unitOfWork.ShoppingCart.GetAll(c => c.ApplicationUserId == claim.Value)
                    .ToList().Count();
                HttpContext.Session.SetInt32(SD.ssShoppingCart, count);
            }
            

            return View(movieList);
            
        }

        public IActionResult Details(int id)
        {
            IEnumerable<Movie> movieList = _unitOfWork.Movie.GetAll(includeProperties: "Genre,AgeGroup");
            IEnumerable<Event> eventList = _unitOfWork.Event.GetAll(includeProperties: "Movie,Hall");

            var movieFromDb = _unitOfWork.Movie.
                         GetFirstOrDefault(u => u.Id == id, includeProperties:"Genre,AgeGroup");
            var eventFromDb = _unitOfWork.Event.
                    GetFirstOrDefault(u => u.MovieId == id, includeProperties:"Movie,Hall");
            
            ShoppingCart cartObj = new ShoppingCart()
            {
                Event = eventFromDb,
                // Movie = movieFromDb
            
            };
            return View(eventFromDb);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        // Moet deze authorize blijven staan als mensen niet willen inloggen?
        [Authorize]
        public IActionResult Details(ShoppingCart CartObject)
        {
            CartObject.Id = 0;
            if (ModelState.IsValid)
            {
                ///add to cart
                var claimsIdentity = (ClaimsIdentity) User.Identity;
                var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
                CartObject.ApplicationUserId = claim.Value;

                
                ShoppingCart cartFromDb = _unitOfWork.ShoppingCart.GetFirstOrDefault(
                    u => u.ApplicationUserId == CartObject.ApplicationUserId
                         && u.EventId == CartObject.EventId,includeProperties:"Event"
                );

                if (cartFromDb == null)
                {
                    ///no record exists
                    _unitOfWork.ShoppingCart.Add(CartObject);
                }
                else
                {
                    cartFromDb.CountAdult += CartObject.CountAdult;
                    cartFromDb.CountEldKids += CartObject.CountEldKids;
                    _unitOfWork.ShoppingCart.Update(cartFromDb);

                }

                _unitOfWork.Save();
                var count = _unitOfWork.ShoppingCart
                    .GetAll(c => c.ApplicationUserId == CartObject.ApplicationUserId)
                    .ToList().Count();

                HttpContext.Session.SetObject(SD.ssShoppingCart, count);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                var eventFromDb = _unitOfWork.Event.GetFirstOrDefault(u => u.Id == CartObject.EventId,
                    includeProperties: "Movie,Hall");

                ShoppingCart cartObj = new ShoppingCart()
                {
                    Event = eventFromDb,
                    EventId = eventFromDb.Id,
                    // Movie = movieFromDb
                
                };
                return View(cartObj
                );
                // }


            }
        }

        public IActionResult Contact()
        {
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
