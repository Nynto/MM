using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using MovieMakers.DataAccess.Repository.IRepository;
using MovieMakers.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieMakers.DataAccess.Data;
using MovieMakers.Utility;

namespace BulkyBook.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin + "," + SD.Role_BO_Employee)]
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _db;


        public UserController(ApplicationDbContext db)
        {
            _db = db;
         
        }
        // GET
        public IActionResult Index()
        {
            return View();
        }
        
        
        
        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
        {
            var userList = _db.ApplicationUsers.Include(u=>u.Company).ToList();
            var userRole = _db.UserRoles.ToList();
            var roles = _db.Roles.ToList();
            foreach (var user in userList)
            {
                var roleId = userRole.FirstOrDefault(u => u.UserId == user.Id).RoleId;
                user.Role = roles.FirstOrDefault(u => u.Id == roleId).Name;
                if (user.Company == null)
                {
                    user.Company = new Company()
                    {
                        Name = ""
                    };
                }
            }
            return Json(new {data = userList});
        }

        [HttpPost]
        public IActionResult LockUnlock([FromBody] string id)
        {
            var objFromDb = _db.ApplicationUsers.FirstOrDefault(u => u.Id == id);
            if (objFromDb == null)
            {
                return Json(new {success = false, message = "Error while Locking/Unlocking"});
            }

            if (objFromDb.LockoutEnd != null && objFromDb.LockoutEnd > DateTime.Now)
            {
                objFromDb.LockoutEnd = DateTime.Now;
            }
            else
            {
                objFromDb.LockoutEnd = DateTime.Now.AddMinutes(1);
            }

            _db.SaveChanges();
            return Json(new {success = true, message = "Operation Successful"});
        }
        
        public static async Task<IdentityResult> DeleteUserAccount(UserManager<ApplicationUser> userManager, 
            string userEmail, ApplicationDbContext context)
        {
            IdentityResult rc = new IdentityResult();

            if ((userManager != null) && (userEmail != null) && (context != null) )
            {
                var user = await userManager.FindByEmailAsync(userEmail);
                var rolesForUser = await userManager.GetRolesAsync(user);

                using (var transaction = context.Database.BeginTransaction())
                {
   

                    if (rolesForUser.Count() > 0)
                    {
                        foreach (var item in rolesForUser.ToList())
                        {
                            // item should be the name of the role
                            var result = await userManager.RemoveFromRoleAsync(user, item);
                        }
                    }
                    rc = await userManager.DeleteAsync(user);
                    transaction.Commit();
                }
            }
            return rc;
        }
        
        // [HttpDelete]
        // public saIActionResult Delete(int id, UserManager<ApplicationUser> userManager, string userEmail, ApplicationDbContext context)
        // {
        //     IdentityResult rc = new IdentityResult();
        //
        //     if ((userManager != null) && (userEmail != null) && (context != null) )
        //     {
        //         var user =  userManager.FindByIdAsync(userEmail);
        //         var rolesForUser =  userManager.GetRolesAsync(user);
        //
        //         using (var transaction = context.Database.BeginTransaction())
        //         {
        //
        //
        //             if (rolesForUser.Count() > 0)
        //             {
        //                 foreach (var item in rolesForUser.ToList())
        //                 {
        //                     // item should be the name of the role
        //                     var result = await userManager.RemoveFromRoleAsync(user, item);
        //                 }
        //             }
        //             rc = userManager.DeleteAsync(user);
        //             transaction.Commit();
        //         }
        //     }
        //     return rc;
        //     var objFromDb = _unitOfWork.U.Get(id);
        //     if (objFromDb == null)
        //     {
        //         return Json(new {success = false, message = "Error while deleting"});
        //     }
        //     string webRootPath = _hostEnvironment.WebRootPath;
        //     var imagePath = Path.Combine(webRootPath, objFromDb.ImageUrl.TrimStart('\\'));
        //     if (System.IO.File.Exists(imagePath))
        //     {
        //         System.IO.File.Delete(imagePath);
        //     }
        //     _unitOfWork.Movie.Remove(objFromDb);
        //     _unitOfWork.Save();
        //     return Json(new {success = true, message = "Delete Successful"});
        // }
        
        
        #endregion
    }
}