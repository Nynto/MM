using System;
using MovieMakers.Models;

namespace MovieMakers.DataAccess.Repository.IRepository
{
    public interface IMovieRepository : IRepository<Movie>
    {
        void Update(Movie movie);
        object Select(Func<object, object> func);
    }
}