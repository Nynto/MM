using System.Linq;
using MovieMakers.Models;
using MovieMakers.DataAccess.Data;
using MovieMakers.DataAccess.Repository.IRepository;

namespace MovieMakers.DataAccess.Repository
{
    public class HallRepository : Repository<Hall>, IHallRepository
    {
        private readonly ApplicationDbContext _db;

        public HallRepository(ApplicationDbContext db):base(db)
        {
            _db = db;
        }

        public void Update(Hall hall)
        {
            var objFromDB = _db.Halls.FirstOrDefault(s => s.Id == hall.Id);
            if (objFromDB != null)
            {
                objFromDB.Name = hall.Name;
                objFromDB.NumberOfRows = hall.NumberOfRows;
            }
        }
    }
}