using System;
using System.Linq;
using MovieMakers.Models;
using MovieMakers.DataAccess.Data;
using MovieMakers.DataAccess.Repository.IRepository;

namespace MovieMakers.DataAccess.Repository
{
    public class MovieRepository : Repository<Movie>, IMovieRepository
    {
        private readonly ApplicationDbContext _db;

        public MovieRepository(ApplicationDbContext db):base(db)
        {
            _db = db;
        }

        public void Update(Movie movie)
        {
            var objFromDB = _db.Movies.FirstOrDefault(s => s.Id == movie.Id);
            if (objFromDB != null)
            {
                if (movie.ImageUrl != null)
                {
                    objFromDB.ImageUrl = movie.ImageUrl;
                }
                objFromDB.IMDB = movie.IMDB;
                objFromDB.Price = movie.Price;
                objFromDB.ListPrice = movie.ListPrice;
                objFromDB.Duration = movie.Duration;
                objFromDB.Title = movie.Title;
                objFromDB.Description = movie.Description;
                objFromDB.GenreId = movie.GenreId;
                objFromDB.Director = movie.Director;
                objFromDB.AgeGroupId = movie.AgeGroupId;
                
            }

        }

        public object Select(Func<object, object> func)
        {
            throw new NotImplementedException();
        }
    }
}