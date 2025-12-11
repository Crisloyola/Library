using Library.Domain.Entities;
using Library.Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Library.Infrastructure.Persistence.Context
{
    public class ApplicationDbContext : DbContext
    {
        public  ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { 
        }

        public DbSet<Books> Books {get; set;}
        public DbSet<Loans> Loans {get; set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new BooksConfiguration());
            modelBuilder.ApplyConfiguration(new LoansConfiguration());
        }
        
    }
}