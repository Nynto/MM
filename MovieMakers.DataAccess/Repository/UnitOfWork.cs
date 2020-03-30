using MovieMakers.Models;
using Microsoft.EntityFrameworkCore;
using MovieMakers.DataAccess.Data;
using MovieMakers.DataAccess.Repository.IRepository;

namespace MovieMakers.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Genre = new GenreRepository(_db);
            Event = new EventRepository(_db);
            Hall = new HallRepository(_db);
            Row = new RowRepository(_db);
            Seat = new SeatRepository(_db);
            Reservation = new ReservationRepository(_db);
            LostAndFound = new LostAndFoundRepository(_db);
            AgeGroup = new AgeGroupRepository(_db);
            Movie = new MovieRepository(_db);
            Company = new CompanyRepository(_db);
            SP_Call = new SP_Call(_db);
            OrderDetails = new OrderDetailsRepository(_db);
            OrderHeader = new OrderHeaderRepository(_db);
            ShoppingCart = new ShoppingCartRepository(_db);
            StartTime = new StartTimeRepository(_db);
            ApplicationUser = new ApplicationUserRepository(_db);
        }

        public IGenreRepository Genre { get; set; }
        public IEventRepository Event { get; set; }
        public IStartTimeRepository StartTime { get; set; }

        public IHallRepository Hall { get; set; }
        public IRowRepository Row { get; set; }
        public ISeatRepository Seat { get; set; }
        public IReservationRepository Reservation { get; set; }

        public ILostAndFoundRepository LostAndFound { get; set; }

        public IApplicationUserRepository ApplicationUser { get; set; }

        public ISP_Call SP_Call { get; private set; }

        public IAgeGroupRepository AgeGroup { get; private set; }
        public IMovieRepository Movie { get; set; }
        public ICompanyRepository Company { get; private set; }
        
        public IShoppingCartRepository ShoppingCart { get; private set; }
        public IOrderHeaderRepository OrderHeader { get; private set; }
        public IOrderDetailsRepository OrderDetails { get; private set; }



        public void Dispose()
        {
            _db.Dispose();
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}