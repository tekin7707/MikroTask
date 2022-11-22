using Mikro.Task.Services.Application.Dtos;
using Mikro.Task.Services.Application.Services.Interfaces;
using Mikro.Task.Services.Db;
using Mikro.Task.Services.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Mikro.Task.Services.Application.Services
{
    public class TheMovieService : ITheMovieService
    {
        private readonly MovieDbContext _movieDbContext;
        public TheMovieService(MovieDbContext movieDbContext)
        {
            _movieDbContext = movieDbContext;
        }

        public async Task<bool> AddRangeAsync(List<TheMovieModel> movies)
        {
            foreach (var movie in movies)
            {
                var dbModel = new MovieModel
                {
                    the_movie_id = movie.id,
                    adult = movie.adult,
                    backdrop_path = movie.backdrop_path,
                    original_language = movie.original_language,
                    original_title = movie.original_title,
                    overview = movie.overview,
                    popularity = movie.popularity,
                    poster_path = movie.poster_path,
                    release_date = movie.release_date,
                    title = movie.title,
                    video = movie.video,
                    vote_average = movie.vote_average,
                    vote_count = movie.vote_count,
                    vote_user=0
                };
                _movieDbContext.Movies.Add(dbModel);
            }
            try
            {
                await _movieDbContext.SaveChangesAsync();

            }
            catch (Exception ex)
            {

                throw;
            }

            return true;
        }
    }
}
