using System.Linq;
using MovieMakers.Models;
using MovieMakers.DataAccess.Data;
using MovieMakers.DataAccess.Repository.IRepository;

namespace MovieMakers.DataAccess.Repository
{
    public class AgeGroupRepository: Repository<AgeGroup>, IAgeGroupRepository
    {
        private readonly ApplicationDbContext _db;

            public AgeGroupRepository(ApplicationDbContext db):base(db)
            {
                _db = db;
            }

            public void Update(AgeGroup ageGroup)
            {
                var objFromDB = _db.AgeGroups.FirstOrDefault(s => s.Id == ageGroup.Id);
                if (objFromDB != null)
                {
                    objFromDB.Name = ageGroup.Name;
                }

            }
            
        
    }
}