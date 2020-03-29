using MovieMakers.Models;
using MovieMakers.Models.ViewModels;

namespace MovieMakers.DataAccess.Repository.IRepository
{
    public interface IApplicationUserRepository : IRepository<ApplicationUser>
    {
        void Update(ApplicationUser applicationUser);
    }
}