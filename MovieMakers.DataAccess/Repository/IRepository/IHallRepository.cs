using MovieMakers.Models;

namespace MovieMakers.DataAccess.Repository.IRepository
{
    public interface IHallRepository : IRepository<Hall>
    {
        void Update(Hall hall);
    }
}