using System.Linq;
using MovieMakers.Models;
using MovieMakers.DataAccess.Data;
using MovieMakers.DataAccess.Repository.IRepository;
using MovieMakers.Models.ViewModels;

namespace MovieMakers.DataAccess.Repository
{
    public class ApplicationUserRepository : Repository<ApplicationUser>, IApplicationUserRepository
    {
        private readonly ApplicationDbContext _db;

        public ApplicationUserRepository(ApplicationDbContext db):base(db)
        {
            _db = db;
        }

        public void Update(ApplicationUser applicationUser)
        {
            var objFromDB = _db.ApplicationUsers.FirstOrDefault(s => s.Id == applicationUser.Id);
            if (objFromDB != null)
            {
                objFromDB.Name = applicationUser.Name;
            }

        }
    }
}