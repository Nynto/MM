using System.Linq;
using MovieMakers.Models;
using MovieMakers.DataAccess.Data;
using MovieMakers.DataAccess.Repository.IRepository;
using MovieMakers.Models.ViewModels;

namespace MovieMakers.DataAccess.Repository
{
    public class StartTimeRepository : Repository<StartTime>, IStartTimeRepository
    {
        private readonly ApplicationDbContext _db;

        public StartTimeRepository(ApplicationDbContext db):base(db)
        {
            _db = db;
        }

        public void Update(StartTime startTime)
        {
            var objFromDB = _db.Genres.FirstOrDefault(s => s.Id == startTime.Id);
            if (objFromDB != null)
            {
                objFromDB.Name = startTime.Name;
            }

        }
    }
}