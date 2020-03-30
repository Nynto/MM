using System.Linq;
using MovieMakers.Models;
using MovieMakers.DataAccess.Data;
using MovieMakers.DataAccess.Repository.IRepository;

namespace MovieMakers.DataAccess.Repository
{
    public class ReservationRepository : Repository<Reservation>, IReservationRepository
    {
        private readonly ApplicationDbContext _db;

        public ReservationRepository(ApplicationDbContext db):base(db)
        {
            _db = db;
        }

        public void Update(Reservation reservation)
        {
            var objFromDB = _db.Reservations.FirstOrDefault(s => s.Id == reservation.Id);
            if (objFromDB != null)
            {
                objFromDB.EventId = reservation.EventId;
                objFromDB.SeatId = reservation.SeatId;
            }
        }
    }
}