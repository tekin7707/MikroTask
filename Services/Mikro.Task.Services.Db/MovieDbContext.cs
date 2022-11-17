

using Microsoft.EntityFrameworkCore;
using Mikro.Task.Services.Domain;

namespace Mikro.Task.Services.Db
{
    public class MovieDbContext:DbContext
    {
        private const string DEF_SCHEMA = "book";
        public MovieDbContext(DbContextOptions<MovieDbContext> options):base(options)
        {
        }

        public DbSet<MovieModel> Movies { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MovieModel>().ToTable("Movies", DEF_SCHEMA);
        }
    }
}