using System;
using MovieMakers.DataAccess.Repository.IRepository;

namespace MovieMakers.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        public IGenreRepository Genre { get; set; }
        public IEventRepository Event { get; set; }
        public IStartTimeRepository StartTime { get; set; }
        public IApplicationUserRepository ApplicationUser { get; set; }


        public IHallRepository Hall { get; set; }

        public IAgeGroupRepository AgeGroup { get; }
        
        public IMovieRepository Movie { get; set; }

        public ICompanyRepository Company { get; }
        public IShoppingCartRepository ShoppingCart { get; }
        public IOrderHeaderRepository OrderHeader { get; }
        public IOrderDetailsRepository OrderDetails { get; }
        
        public ISP_Call SP_Call
        {
            get ; 
        }

        void Save();
    }
}