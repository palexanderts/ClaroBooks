using Data.EntityTypeConfigurations;
using Domain.Books;
using Microsoft.EntityFrameworkCore;
using System;

namespace Data
{
    public sealed class BoocksDbContext : DbContext
    {
        public DbSet<Book> Books { get; set; }

        public BoocksDbContext(DbContextOptions<BoocksDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BooksEntityTypeConfiguration());
        }
    }
}
