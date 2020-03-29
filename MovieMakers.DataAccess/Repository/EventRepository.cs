using System.Linq;
using MovieMakers.Models;
using MovieMakers.DataAccess.Data;
using MovieMakers.DataAccess.Repository.IRepository;
using MovieMakers.Models.ViewModels;

namespace MovieMakers.DataAccess.Repository
{
    public class EventRepository : Repository<Event>, IEventRepository
    {
        private readonly ApplicationDbContext _db;

        public EventRepository(ApplicationDbContext db):base(db)
        {
            _db = db;
        }

        public void Update(Event eEvent)
        {
            var objFromDB = _db.Events.FirstOrDefault(s => s.Id == eEvent.Id);
            if (objFromDB != null)
            {
                objFromDB.Id = eEvent.Id;
            }
            objFromDB.StartTime = eEvent.StartTime;
            objFromDB.HallId = eEvent.HallId;
            objFromDB.Date = eEvent.Date;
            objFromDB.MovieId = eEvent.MovieId;
        }
    }
}