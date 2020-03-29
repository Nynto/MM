using MovieMakers.Models;

namespace MovieMakers.DataAccess.Repository.IRepository
{
    public interface IAgeGroupRepository : IRepository<AgeGroup>
    {
        void Update(AgeGroup ageGroup);
    }
    
}