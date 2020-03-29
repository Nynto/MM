using System.Linq;
using MovieMakers.Models;
using MovieMakers.DataAccess.Data;
using MovieMakers.DataAccess.Repository.IRepository;

namespace MovieMakers.DataAccess.Repository
{
    public class GenreRepository : Repository<Genre>, IGenreRepository
    {
        private readonly ApplicationDbContext _db;

        public GenreRepository(ApplicationDbContext db):base(db)
        {
            _db = db;
        }

        public void Update(Genre genre)
        {
            var objFromDB = _db.Genres.FirstOrDefault(s => s.Id == genre.Id);
            if (objFromDB != null)
            {
                objFromDB.Name = genre.Name;
            }

        }
    }
}