using MovieMakers.Models;

namespace MovieMakers.DataAccess.Repository.IRepository
{
    public interface IRowRepository : IRepository<Row>
    {
        void Update(Row row);
    }
}