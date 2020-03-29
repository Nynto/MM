using System.Linq;
using MovieMakers.Models;
using MovieMakers.DataAccess.Data;
using MovieMakers.DataAccess.Repository.IRepository;

namespace MovieMakers.DataAccess.Repository
{
    public class SeatRepository : Repository<Seat>, ISeatRepository
    {
        private readonly ApplicationDbContext _db;

        public SeatRepository(ApplicationDbContext db):base(db)
        {
            _db = db;
        }

        public void Update(Seat seat)
        {
            var objFromDB = _db.Seats.FirstOrDefault(s => s.Id == seat.Id);
            if (objFromDB != null)
            {
                objFromDB.HallId = seat.HallId;
                objFromDB.Row = seat.Row;
                objFromDB.Number = seat.Number;
            }
        }
    }
}