using MovieMakers.Models;

namespace MovieMakers.DataAccess.Repository.IRepository
{
    public interface ILostAndFoundRepository : IRepository<LostAndFound>
    {
        void Update(LostAndFound lostAndFound);
    }
}