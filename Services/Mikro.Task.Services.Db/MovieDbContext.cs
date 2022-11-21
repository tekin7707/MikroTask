

using Microsoft.EntityFrameworkCore;
using Mikro.Task.Services.Application.Dtos;
using Mikro.Task.Services.Domain;

namespace Mikro.Task.Services.Db
{
    public class MovieDbContext:DbContext
    {
        public MovieDbContext(DbContextOptions<MovieDbContext> options):base(options)
        {
        }

        public DbSet<MovieModel> Movies { get; set; }
        public DbSet<MovieCommentModel> Comments { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MovieModel>().ToTable("Movies", "mov");
            modelBuilder.Entity<MovieCommentModel>().ToTable("Comments", "mov");
        }
    }
}