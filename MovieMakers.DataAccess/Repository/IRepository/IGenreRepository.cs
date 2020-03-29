using MovieMakers.Models;

namespace MovieMakers.DataAccess.Repository.IRepository
{
    public interface IGenreRepository : IRepository<Genre>
    {
        void Update(Genre genre);
    }
}