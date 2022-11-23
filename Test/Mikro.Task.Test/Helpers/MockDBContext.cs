using Microsoft.EntityFrameworkCore;
using Mikro.Task.Services.Db;
using Mikro.Task.Services.Domain;

namespace Mikro.Task.Test.Helpers
{
    public class MockDBContext
    {
        public static MovieDbContext Get()
        {
            var options = new DbContextOptionsBuilder<MovieDbContext>()
              .UseInMemoryDatabase(Guid.NewGuid().ToString())
              .Options;

            var context = new MovieDbContext(options);
            context.Database.EnsureCreated();
            Fill(context);
            return context;
        }

        public static void Fill(MovieDbContext context)
        {
            for (int i = 0; i < 20; i++)
            {
                context.Movies.Add(new MovieModel
                {
                    id = i + 1,
                    title = $"Movie {i + 1}",
                    adult = i % 6 == 0,
                    original_language = "en",
                    original_title = $"Movie {i + 1}",
                    the_movie_id = 1001 + i,
                    release_date = i % 2 == 0 ? "11/21/2022" : "01/01/2019",
                    overview = "overview",
                    backdrop_path = "",
                    poster_path = ""
                });
            }
            context.SaveChanges();
        }
    }
}
