using System;
using System.Collections.Generic;
using System.Text;
using MovieMakers.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MovieMakers.Models.ViewModels;

namespace MovieMakers.DataAccess.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Genre> Genres { get; set; }
        public DbSet<Event> Events { get; set; }

        public DbSet<StartTime> Times { get; set; }

        public DbSet<AgeGroup> AgeGroups { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Company> Companies { get; set; }

        public DbSet<Hall> Halls { get; set; }
        public DbSet<Seat> Seats { get; set; }

        public DbSet<LostAndFound> LostAndFounds { get; set; }

        public DbSet<OrderHeader> OrderHeaders { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
    }



}
