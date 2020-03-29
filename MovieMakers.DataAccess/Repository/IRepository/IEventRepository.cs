using MovieMakers.Models;
using MovieMakers.Models.ViewModels;

namespace MovieMakers.DataAccess.Repository.IRepository
{
    public interface IEventRepository : IRepository<Event>
    {
        void Update(Event eEvent);
    }
}