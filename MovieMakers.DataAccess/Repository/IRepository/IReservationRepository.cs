using MovieMakers.Models;

namespace MovieMakers.DataAccess.Repository.IRepository
{
    public interface IReservationRepository : IRepository<Reservation>
    {
        void Update(Reservation reservation);
    }
}