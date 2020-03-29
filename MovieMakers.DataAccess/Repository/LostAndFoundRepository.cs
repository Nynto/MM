using System.Linq;
using MovieMakers.Models;
using MovieMakers.DataAccess.Data;
using MovieMakers.DataAccess.Repository.IRepository;
using MovieMakers.Models.ViewModels;

namespace MovieMakers.DataAccess.Repository
{
    public class LostAndFoundRepository : Repository<LostAndFound>, ILostAndFoundRepository
    {
        private readonly ApplicationDbContext _db;

        public LostAndFoundRepository(ApplicationDbContext db):base(db)
        {
            _db = db;
        }

        public void Update(LostAndFound lostAndFound)
        {
            var objFromDB = _db.LostAndFounds.FirstOrDefault(s => s.Id == lostAndFound.Id);
            if (objFromDB != null)
            {
                objFromDB.Time = lostAndFound.Time;
                objFromDB.Location = lostAndFound.Location;
                objFromDB.Description = lostAndFound.Description;
                objFromDB.Solved = lostAndFound.Solved;
            }
        }
    }
}