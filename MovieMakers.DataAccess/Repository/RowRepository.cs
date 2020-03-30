using System.Linq;
using MovieMakers.Models;
using MovieMakers.DataAccess.Data;
using MovieMakers.DataAccess.Repository.IRepository;

namespace MovieMakers.DataAccess.Repository
{
    public class RowRepository : Repository<Row>, IRowRepository
    {
        private readonly ApplicationDbContext _db;

        public RowRepository(ApplicationDbContext db):base(db)
        {
            _db = db;
        }

        public void Update(Row row)
        {
            var objFromDB = _db.Rows.FirstOrDefault(s => s.Id == row.Id);
            if (objFromDB != null)
            {
                objFromDB.HallId = row.HallId;
                objFromDB.Number = row.Number;
                objFromDB.NumberOfSeats = row.NumberOfSeats;
            }
        }
    }
}