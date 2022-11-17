using Mikro.Task.Services.Application.Dtos;
using Mikro.Task.Services.Db;
using Mikro.Task.Services.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mikro.Task.Services.Application.Services
{
    public class TheMovieService : ITheMovieService
    {
        private readonly MovieDbContext _db;

        public TheMovieService(MovieDbContext db)
        {
            _db = db;
        }

        //public async Task<Result> AddAsync(Result model)
        //{
        //    var dbModel = _db.Movies.FirstOrDefault(x => x.the_movie_id == model.id);
        //    if (dbModel == null)
        //    {
        //        dbModel = new MovieModel
        //        {
        //            the_movie_id = model.id,
        //            adult = model.adult,
        //            backdrop_path = model.backdrop_path,
        //            original_language = model.original_language,
        //            original_title = model.original_title,
        //            overview = model.overview,
        //            popularity = model.popularity,
        //            poster_path = model.poster_path,
        //            release_date = model.release_date,
        //            title = model.title,
        //            video = model.video,
        //            vote_average = model.vote_average,
        //            vote_count = model.vote_count
        //        };
        //        _db.Movies.Add(dbModel);

        //        await _db.SaveChangesAsync();
        //        model.dbid = dbModel.id;
        //    }
        //    else
        //        model.dbid = dbModel.id;

        //    return model;
        //}

        //public Task<bool> AddRangeAsync(List<Result> model)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
