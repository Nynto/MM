using MovieMakers.Models;

namespace MovieMakers.DataAccess.Repository.IRepository
{
    public interface ISeatRepository : IRepository<Seat>
    {
        void Update(Seat seat);
    }
}