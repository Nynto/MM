using MovieMakers.Models;
using MovieMakers.Models.ViewModels;

namespace MovieMakers.DataAccess.Repository.IRepository
{
    public interface IStartTimeRepository : IRepository<StartTime>
    {
        void Update(StartTime startTime);
    }
}